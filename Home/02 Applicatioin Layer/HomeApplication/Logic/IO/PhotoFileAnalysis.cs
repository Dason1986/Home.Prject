using DomainModel.Aggregates.GalleryAgg;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using Library;
using NLog.Fluent;
using Repository;
using Repository.ModuleProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using static Library.Draw.ImageExif;

namespace HomeApplication.Logic.IO
{

    public class PhotoFileAnalysis : BaseMultiThreadingLogicService
    {
        public   PhotoFileAnalysisOption Option
        {
            get { return _option; }
            set
            {
                _option = value;
              
            }
        }

        PhotoFileAnalysisOption _option;
        protected override IOption ServiceOption
        {
            get
            {
                return Option;
            }

            set
            {

                Option = (PhotoFileAnalysisOption)value;
            }
        }



        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
            #region MyRegion


            var take = endindex - beginindex;

             
            {
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();


                var photolist = _filesRepository.GetFilesByExtensions(Option.ImageTypes).Skip(beginindex).Take(take).ToList();
                //index = index + size;
                foreach (var item in photolist)
                {

                    Image image = null;
                    Photo photo = null;

                    if (item.Photo == null)
                    {

                        if (!System.IO.File.Exists(item.FullPath)) continue;
                        Logger.Info(item.FileName);


                        #region MyRegion

                        try
                        {
                            image = Image.FromFile(item.FullPath);
                        }
                        catch (Exception ex)
                        {
                            // Console.WriteLine(ex);
                            Logger.Error(ex, item.FullPath + "文件不能以图像形式打开！");
                            continue;

                        }
                        photo = new Photo(CreatedInfo.PhotoFileAnalysis)
                        {
                            FileID = item.ID,
                            File = item,
                            PhotoType = DomainModel.PhotoType.Graphy,
                        };

                        item.Photo = photo;
                        #endregion
                    }
                    else
                    {
                        if (!System.IO.File.Exists(item.FullPath)) continue;
                        //    Console.WriteLine(item.FileName);
                        image = Image.FromFile(item.FullPath);
                        photo = item.Photo;
                        if (photo.Attributes != null && photo.Attributes.Count > 0) continue;
                    }
                    #region MyRegion

                    Library.Draw.ImageExif exif = GetExif(item, image);
                    ICollection<PhotoAttribute> attributes = new List<PhotoAttribute>();
                    photo.Attributes = attributes;
                    if (exif != null)
                    {
                        DoImageExif(photo, exif);
                    }
                    if (exif == null || !image.PropertyIdList.Contains((int)PropertyTagId.ThumbnailData))
                    {
                        CreateThumbnail(photo, image);

                    }




                    #endregion

                    _filesRepository.UnitOfWork.Commit();


                }
                #endregion
            }
        }

        private static void CreateThumbnail(Photo photo, Image image)
        {
            ICollection<PhotoAttribute> attributes = photo.Attributes;
            var ThumbnailWidth = 0;
            var ThumbnailHeight = 0;
            if (image.Width < 150 && image.Height < 150)
            {
                ThumbnailWidth = image.Width;
                ThumbnailHeight = image.Height;
            }
            else if (image.Width > image.Height)
            {
                var per = (decimal)150 / image.Width;
                ThumbnailWidth = 150;

                ThumbnailHeight = (int)(image.Height * per);
            }
            else
            {
                var per = (decimal)150 / image.Height;
                ThumbnailHeight = 150;

                ThumbnailWidth = (int)(image.Width * per);
            }
            var ThumbnailImage = image.GetThumbnailImage(ThumbnailWidth, ThumbnailHeight, () => { return false; }, IntPtr.Zero);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ThumbnailImage.Save(ms, ImageFormat.Jpeg);
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = PropertyTagId.ThumbnailWidth.ToString(), AttValue = ThumbnailWidth.ToString() });
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = PropertyTagId.ThumbnailHeight.ToString(), AttValue = ThumbnailHeight.ToString() });
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = PropertyTagId.ThumbnailData.ToString(), BitValue = ms.ToArray() });
            ms.Dispose();
            ThumbnailImage.Dispose();
        }

        private static void DoImageExif(Photo photo, Library.Draw.ImageExif exif)
        {
            ICollection<PhotoAttribute> attributes = photo.Attributes;
            foreach (ExifProperty exifitemProperty in exif.Properties)
            {

                if (exifitemProperty.Value == null || exifitemProperty.Value.Length == 0) continue;
                var additemExif = new PhotoAttribute(CreatedInfo.PhotoFileAnalysis)
                {
                    PhotoID = photo.ID,
                    AttKey = exifitemProperty.TagId.ToString(),

                };




                if (exifitemProperty.Type == PropertyTagType.Byte)
                {
                    byte[] temp = new byte[exifitemProperty.Len];
                    Array.Copy(exifitemProperty.Value, temp, exifitemProperty.Len);

                    additemExif.BitValue = temp;
                }
                else
                {
                    var obj = exifitemProperty.DisplayValue;
                    if (obj != null)
                        additemExif.AttValue = obj.ToString().Trim();
                    else additemExif.BitValue = exifitemProperty.Value;
                }


                // if (additemExif.AttValue.Length < 255)


                attributes.Add(additemExif);

            }
        }

        private Library.Draw.ImageExif GetExif(DomainModel.Aggregates.FileAgg.FileInfo item, Image image)
        {
            try
            {
                return Library.Draw.ImageExif.GetExifInfo(image);
            }
            catch (Exception ex)
            {


                //    Console.WriteLine(ex);
                Logger.Error(ex, item.FullPath + "图像无法分析！");


            }

            return null;
        }

        protected override int GetTotalRecord()
        {
            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            IFileInfoRepository _filesRepository = provider.CreateFileInfo();

            var filecount = _filesRepository.GetFilesByExtensions(Option.ImageTypes).Count();
            return filecount;
        }
    }

    public struct PhotoFileAnalysisOption : IOption
    {

        public string[] ImageTypes { get; set; }
    }

    public class PhotoFileAnalysisOptionCommandBuilder : IOptionCommandBuilder<PhotoFileAnalysisOption>
    {
        public PhotoFileAnalysisOption GetOption()
        {
            return _option;
        }
        PhotoFileAnalysisOption _option;
        IOption IOptionCommandBuilder.GetOption()
        {
            return _option;
        }
        public void RumCommandLine()
        {
            _option = new PhotoFileAnalysisOption();
            Console.Write("是否使用默认条件（Y）：");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };

                return;
            }
            Console.WriteLine();
            LabCmd:
            Console.Write("輸入圖像類型（,分隔）：");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("不能爲空");
                goto LabCmd;
            }

            _option.ImageTypes = path.Split(',');

        }
    }

}
