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
//using static Library.Draw.ImageExif;

namespace HomeApplication.Logic.IO
{

    public class PhotoFileAnalysis : BaseMultiThreadingLogicService
    {
        public PhotoFileAnalysisOption Option
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
        PhotoFileAnalysisProvider photoFileAnalysisProvider;

        protected override bool OnVerification()
        {
            switch (Option.SourceType)
            {
                case PhotoFileAnalysisSrouceType.Db:
                    photoFileAnalysisProvider = new PhotoFileAnalysisByDb(this);
                    break;
                case PhotoFileAnalysisSrouceType.File:
                    photoFileAnalysisProvider = new PhotoFileAnalysisByConfigFile(this);
                    break;
                default:
                    break;
            }
            return base.OnVerification();
        }
        protected override int GetTotalRecord()
        {
            return photoFileAnalysisProvider.GetTotalRecord();
        }
        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            photoFileAnalysisProvider.ThreadProssSize(beginindex, endindex);
        }

        protected void CreateThumbnail(Photo photo, Image image)
        {
            ICollection<PhotoAttribute> attributes = photo.Attributes;
            var ThumbnailWidth = 0;
            var ThumbnailHeight = 0;
            if (image.Width < 120 && image.Height < 120)
            {
                ThumbnailWidth = image.Width;
                ThumbnailHeight = image.Height;
            }
            else if (image.Width > image.Height)
            {
                var per = (decimal)120 / image.Width;
                ThumbnailWidth = 120;

                ThumbnailHeight = (int)(image.Height * per);
            }
            else
            {
                var per = (decimal)120 / image.Height;
                ThumbnailHeight = 120;

                ThumbnailWidth = (int)(image.Width * per);
            }

            var thumbnailImage = image.GetThumbnailImage(ThumbnailWidth, ThumbnailHeight, () => { return false; }, IntPtr.Zero);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            thumbnailImage.Save(ms, ImageFormat.Jpeg);
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = Library.Draw.ImageExif.PropertyTagId.ThumbnailWidth.ToString(), AttValue = ThumbnailWidth.ToString() });
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = Library.Draw.ImageExif.PropertyTagId.ThumbnailHeight.ToString(), AttValue = ThumbnailHeight.ToString() });
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = Library.Draw.ImageExif.PropertyTagId.ThumbnailData.ToString(), BitValue = ms.ToArray() });
            ms.Dispose();
            thumbnailImage.Dispose();
        }

        protected void DoImageExif(Photo photo, Library.Draw.ImageExif exif)
        {
            ICollection<PhotoAttribute> attributes = photo.Attributes;

            foreach (Library.Draw.ImageExif.ExifProperty exifitemProperty in exif.Properties)
            {

                if (exifitemProperty.Value == null || exifitemProperty.Value.Length == 0) continue;
                var additemExif = new PhotoAttribute(CreatedInfo.PhotoFileAnalysis)
                {
                    PhotoID = photo.ID,
                    AttKey = exifitemProperty.TagId.ToString(),

                };




                if (exifitemProperty.Type == Library.Draw.ImageExif.PropertyTagType.Byte)
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




        abstract class PhotoFileAnalysisProvider
        {
            protected PhotoFileAnalysis Analysis { get; private set; }
            public PhotoFileAnalysisProvider(PhotoFileAnalysis analysis)
            {
                Analysis = analysis;
            }
            public abstract int GetTotalRecord();
            public abstract void ThreadProssSize(int beginindex, int endindex);
        }

        class PhotoFileAnalysisByDb : PhotoFileAnalysisProvider
        {


            string[] filenames;

            public PhotoFileAnalysisByDb(PhotoFileAnalysis analysis) : base(analysis)
            {
            }


            public override int GetTotalRecord()
            {
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();

                var filecount = _filesRepository.GetFilesByExtensions(Analysis.Option.ImageTypes).Count();
                return filecount;
            }

            public override void ThreadProssSize(int beginindex, int endindex)
            {
                Analysis.Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
                #region MyRegion


                var take = Analysis.BatchSize;



                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();


                var photolist = _filesRepository.GetFilesByExtensions(Analysis.Option.ImageTypes).Skip(beginindex).Take(take).ToList();
                //index = index + size;
                foreach (var item in photolist)
                {

                    Image image = null;
                    Photo photo = item.Photo;

                    var fileinfo = new System.IO.FileInfo(item.FullPath);
                    if (!fileinfo.Exists) continue;
                    if (photo == null)
                    {
                        Analysis.Logger.Info(item.FileName);
                        photo = new Photo(CreatedInfo.PhotoFileAnalysis)
                        {
                            FileID = item.ID,
                            File = item,
                            PhotoType = DomainModel.PhotoType.Graphy,
                        };

                        item.Photo = photo;
                    }

                    if (photo.Attributes != null && photo.Attributes.Count > 0) continue;
                    try
                    {
                        image = new Bitmap(fileinfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
                    }
                    catch (Exception ex)
                    {
                        // Console.WriteLine(ex);
                        Analysis.Logger.Error(ex, item.FullPath + "文件不能以圖像形式打開！");
                        continue;

                    }
                    #region MyRegion
                    try
                    {
                        Library.Draw.ImageExif exif = Library.Draw.ImageExif.GetExifInfo(image);
                        ICollection<PhotoAttribute> attributes = new List<PhotoAttribute>();
                        photo.Attributes = attributes;
                        if (exif != null)
                        {
                            Analysis.DoImageExif(photo, exif);
                        }
                        if (exif == null || !image.PropertyIdList.Contains((int)Library.Draw.ImageExif.PropertyTagId.ThumbnailData))
                        {
                            Analysis.CreateThumbnail(photo, image);

                        }
                    }
                    catch (Exception ex)
                    {

                        Analysis.Logger.Error(ex, item.FullPath + "分析圖像失敗！");
                    }





                    #endregion

                    _filesRepository.UnitOfWork.Commit();


                    if (image != null) image.Dispose();


                }
                #endregion
                GC.Collect();
            }
        }
        class PhotoFileAnalysisByConfigFile : PhotoFileAnalysisProvider
        {


            string[] filenames;

            public PhotoFileAnalysisByConfigFile(PhotoFileAnalysis analysis) : base(analysis)
            {
            }


            public override void ThreadProssSize(int beginindex, int endindex)
            {
                Analysis.Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
                #region MyRegion


                var take = Analysis.BatchSize;



                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();


                var files = filenames.Skip(beginindex).Take(take).ToList();
                //index = index + size;
                foreach (var file in files)
                {
                    DomainModel.Aggregates.FileAgg.FileInfo item = _filesRepository.GetByFullPath(file);
                    if (item == null)
                    {
                        Analysis.Logger.Warn("記錄不存在！"+ file);
                        continue;
                    }
                    Image image = null;
                    Photo photo = item.Photo;

                    var fileinfo = new System.IO.FileInfo(item.FullPath);
                    if (!fileinfo.Exists) continue;
                    if (photo == null)
                    {
                        Analysis.Logger.Info(item.FileName);
                        photo = new Photo(CreatedInfo.PhotoFileAnalysis)
                        {
                            FileID = item.ID,
                            File = item,
                            PhotoType = DomainModel.PhotoType.Graphy,
                        };

                        item.Photo = photo;
                    }

                    if (photo.Attributes != null && photo.Attributes.Count > 0) continue;
                    try
                    {
                        image = new Bitmap(fileinfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
                    }
                    catch (Exception ex)
                    {
                        // Console.WriteLine(ex);
                        Analysis.Logger.Error(ex, item.FullPath + "文件不能以圖像形式打開！");
                        continue;

                    }
                    #region MyRegion
                    try
                    {
                        Library.Draw.ImageExif exif = Library.Draw.ImageExif.GetExifInfo(image);
                        ICollection<PhotoAttribute> attributes = new List<PhotoAttribute>();
                        photo.Attributes = attributes;
                        if (exif != null)
                        {
                            Analysis.DoImageExif(photo, exif);
                        }
                        if (exif == null || !image.PropertyIdList.Contains((int)Library.Draw.ImageExif.PropertyTagId.ThumbnailData))
                        {
                            Analysis.Logger.Info( item.FullPath + "生成縮略圖");
                            Analysis.CreateThumbnail(photo, image);

                        }
                    }
                    catch (Exception ex)
                    {

                        Analysis.Logger.Error(ex, item.FullPath + "分析圖像失敗！");
                    }





                    #endregion

                    _filesRepository.UnitOfWork.Commit();


                    if (image != null) image.Dispose();


                }
                #endregion
                GC.Collect();
            }



            public override int GetTotalRecord()
            {
                var file = Analysis.Option.FileListPath;
                if (System.IO.File.Exists(file))
                {
                    filenames = System.IO.File.ReadAllLines(file);
                    return filenames.Length;
                }
                return 0;
            }
        }
    }



    public struct PhotoFileAnalysisOption : IOption
    {
        public string FileListPath { get; set; }
        public PhotoFileAnalysisSrouceType SourceType { get; set; }
        public string[] ImageTypes { get; set; }
    }
    public enum PhotoFileAnalysisSrouceType
    {
        Db,
        File
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
            Console.WriteLine();
            if (key.Key == ConsoleKey.Y)
            {
                //  Console.WriteLine();
                _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };
                _option.SourceType = PhotoFileAnalysisSrouceType.Db;
                return;
            }

            {
                LabSource:

                Console.Write("圖像文件來源（0:db,1:txt文件）：");
                var sourcetype = Console.ReadLine();
                switch (sourcetype)
                {
                    case "0": _option.SourceType = PhotoFileAnalysisSrouceType.Db; break;
                    case "1": _option.SourceType = PhotoFileAnalysisSrouceType.File; break;
                    default:
                        goto LabSource;
                }

            }
            switch (_option.SourceType)
            {
                case PhotoFileAnalysisSrouceType.Db:
                    {
                        Console.Write("是否使用默认条件（Y）：");

                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            //  Console.WriteLine();
                            _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };

                            return;
                        }
                        LabCmd:
                        Console.Write("輸入圖像類型（,分隔）：");
                        var path = Console.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            Console.WriteLine("不能爲空！");
                            goto LabCmd;
                        }

                        _option.ImageTypes = path.Split(',');
                    }
                    break;
                case PhotoFileAnalysisSrouceType.File:
                    {
                        LabCmd:
                        Console.Write("輸入文件列表路徑：");
                        var path = Console.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            Console.WriteLine("不能爲空！");
                            goto LabCmd;
                        }
                        if (!System.IO.File.Exists(path))
                        {
                            Console.WriteLine("文件不存在！");
                            goto LabCmd;
                        }
                        _option.FileListPath = path;
                    }
                    break;
                default:
                    break;
            }


        }
    }

}
