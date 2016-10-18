using DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.DomainEvents;
using DomainModel.Aggregates.GalleryAgg;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace HomeApplication.DomainServices
{
    public class AddPhotoDomainService : PhotoDomainService, IAddPhotoDomainService
    {
        public void Handle(Photo photo, DomainModel.Aggregates.FileAgg.FileInfo file)
        {
            CurrnetPhoto = photo;
            CurrnetFile = file;
            DoAddAction();
            ModuleProvider.UnitOfWork.Commit();
        }


        protected override void DoAddAction()
        {

            //  System.IO.FileInfo fileinfo = new System.IO.FileInfo(CurrnetFile.FullPath);
            if (!System.IO.File.Exists(CurrnetFile.FullPath))
            {
                Logger.Warn(CurrnetFile.FullPath + "|File not exist");
                return;
            }
            if (CurrnetPhoto == null)
            {
                Logger.Trace(CurrnetFile.FullPath + "|Create Photo Entity");
                CurrnetPhoto = new Photo(CreatedInfo.PhotoFileAnalysis)
                {
                    FileID = CurrnetFile.ID,
                    File = CurrnetFile,
                    PhotoType = DomainModel.PhotoType.Graphy,
                };
                CurrnetFile.Photo = CurrnetPhoto;
            }
            if (CurrnetPhoto.Attributes != null && CurrnetPhoto.Attributes.Count > 0) return;
            Logger.Trace(CurrnetFile.FullPath + "|Analysis");
            Image image;

            Stream fs = null;
            try
            {
                fs = System.IO.File.Open(CurrnetFile.FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                image = new Bitmap(fs);
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex);
                Logger.Error(ex, CurrnetFile.FullPath + "|File can not open！");
                return;

            }

            var exifInfo = Library.Draw.ImageExif.GetExifInfo(image);
            if (CurrnetPhoto.Attributes == null)
            {
                ICollection<PhotoAttribute> attributes = new List<PhotoAttribute>();
                CurrnetPhoto.Attributes = attributes;
            }
            DoImageExif(exifInfo);
            if (exifInfo == null || !image.PropertyIdList.Contains((int)Library.Draw.ImageExif.PropertyTagId.ThumbnailData))
            {
                Logger.Trace(CurrnetFile.FullPath + "|Create Thumbnail");

                CreateThumbnail(image);

            }
            fs.Dispose();
            image.Dispose();
        }




        void IDomainService.Handle(IDomainEventArgs args)
        {
            this.Handle(args as PhotoItemEventArgs);
        }

        protected void CreateThumbnail(Image image)
        {
            var photo = CurrnetPhoto;
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

            Bitmap thumbnailImage = new Bitmap(image, new Size(ThumbnailWidth, ThumbnailHeight));
            MemoryStream ms = new MemoryStream();
            thumbnailImage.Save(ms, ImageFormat.Jpeg);
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = Library.Draw.ImageExif.PropertyTagId.ThumbnailWidth.ToString(), AttValue = ThumbnailWidth.ToString() });
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = Library.Draw.ImageExif.PropertyTagId.ThumbnailHeight.ToString(), AttValue = ThumbnailHeight.ToString() });
            attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis) { PhotoID = photo.ID, AttKey = Library.Draw.ImageExif.PropertyTagId.ThumbnailData.ToString(), BitValue = ms.ToArray() });
            ms.Close();
            ms.Dispose();
            thumbnailImage.Dispose();
        }

        protected void DoImageExif(Library.Draw.ImageExif exif)
        {
            Logger.Trace(CurrnetFile.FullPath + "|Create exif");
            var photo = CurrnetPhoto;
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





                attributes.Add(additemExif);

            }
        }
    }
}
