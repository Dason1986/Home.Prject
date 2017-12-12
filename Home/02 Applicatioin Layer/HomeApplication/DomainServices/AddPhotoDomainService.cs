using Home.DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using Home.DomainModel.Aggregates.GalleryAgg;
using System.Drawing;
using System.IO;
using Library.Infrastructure.Application;
using Home.DomainModel;
using Library.Draw;
using HomeApplication.ComponentModel.IO;
using FileInfo = Home.DomainModel.Aggregates.FileAgg.FileInfo;

namespace HomeApplication.DomainServices
{
    public class AddPhotoDomainService : PhotoDomainService, IAddPhotoDomainService
    {
        public void Handle(Home.DomainModel.Aggregates.FileAgg.FileInfo file)
        {
            CurrnetFile = file;
            if (file != null)
                CurrnetPhoto = file.Photo;
            DoAddAction();
        }
        public AddPhotoDomainService()
        {
            _photoEnvironment = Library.Bootstrap.Currnet.GetService<IPhotoEnvironment>();
        }
        private readonly IPhotoEnvironment _photoEnvironment;
        private Library.Storage.IFileStorage storage;
        private Image image;
        private Stream fs;
        private Orientation imageOrientation;

        protected override void OnDispose()
        {
            if (storage != null) storage.Dispose();
            if (fs != null) fs.Dispose();
            if (image != null) image.Dispose();
            base.OnDispose();
        }

        protected override void DoAddAction()
        {
            if (CurrnetFile == null) return;
            if (!_photoEnvironment.Isloadconfig) _photoEnvironment.LoadConfig(this.GalleryModuleProvider.CreateSystemParameter());
            try
            {

                if (CurrnetPhoto == null)
                {
                    Logger.TraceByContent("Create Photo Entity", CurrnetFile.FullPath);
                    CurrnetPhoto = PhotoRepository.Get(CurrnetFile.ID);
                    if (CurrnetPhoto == null)
                    {
                        CurrnetPhoto = new Photo(CreatedInfo.PhotoFileAnalysis)
                        {
                            ID = CurrnetFile.ID,
                            FileID = CurrnetFile.ID,
                        };
                        CurrnetFile.Photo = CurrnetPhoto;
                        PhotoRepository.Add(CurrnetPhoto);
                    }
                    // CurrnetFile.Photo = CurrnetPhoto;
                }
                using (var storage = CurrnetFile.GetStorage())
                {
                    

                    if (!storage.Exists)
                    {                    
                        throw new PhotoDomainServiceException(Resources.DomainServiceResource.FileNotExist, new FileNotFoundException(CurrnetFile.FullPath));
                    }
                    

                    fs = storage.Get();

                    CurrnetFile.MD5 = Library.HelperUtility.FileUtility.FileMD5(fs);
                    CurrnetFile.FileSize = fs.Length;

                    if (this.FilesRepository.FileExists(CurrnetFile.MD5, CurrnetFile.FileSize))
                    {
                        storage.Delete();
                        CurrnetFile.FileStatue = Home.DomainModel.FileStatue.Duplicate;
                        CurrnetFile.StatusCode = Library.ComponentModel.Model.StatusCode.Delete;
                        this.GalleryModuleProvider.UnitOfWork.Commit();
                        return;
                    }
                    if (CurrnetPhoto.Attributes != null && CurrnetPhoto.Attributes.Count > 0) return;
                    image = new Bitmap(fs);

                    var exifInfo = ImageExif.GetExifInfo(image);
                    if (CurrnetPhoto.Attributes == null)
                    {
                        ICollection<PhotoAttribute> attributes = new List<PhotoAttribute>();
                        CurrnetPhoto.Attributes = attributes;
                    }
                    DoImageExif(image, exifInfo);
                    BuildImage(image);
                    image.Dispose();
                }
            }
            catch (Exception)
            {
                this.GalleryModuleProvider.UnitOfWork.RollbackChanges();
                CurrnetFile.StatusCode = Library.ComponentModel.Model.StatusCode.Disabled;
                this.GalleryModuleProvider.UnitOfWork.Commit();
                throw;
            }
        }

        private void BuildImage(Image image)
        {
            // Logger.TraceByContent("Create Image", CurrnetFile.FullPath);
            var builder = new PhotoStorageBuilder()
            {
                SourceImage = image,
                Storage = this._photoEnvironment.CreateImageStorage(this.CurrnetPhoto.ID),
                IsPanoramic = isPanoramic,
                Orientation = imageOrientation
            };

            var maxLive = builder.Build();
            var photo = CurrnetPhoto;
            ICollection<PhotoAttribute> attributes = photo.Attributes;
            attributes.Add(CreateAtt("ImageLevel", maxLive.ToString()));
        }

        private PhotoAttribute CreateAtt(string key, string value)
        {
            var additemExif = new PhotoAttribute(CreatedInfo.PhotoFileAnalysis)
            {
                OwnerID = CurrnetPhoto.ID,
                AttKey = key,
                AttValue = value
            };
            return additemExif;
        }

        protected void DoImageExif(Image image, Library.Draw.ImageExif exif)
        {
            Logger.Trace("Create exif:{0}", CurrnetFile.FullPath);

            var photo = CurrnetPhoto;
            ICollection<PhotoAttribute> attributes = photo.Attributes;
            attributes.Add(exif.DateTimeDigitized != null
                ? CreateAtt("DateTimeDigitized", exif.DateTimeDigitized.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                : CreateAtt("DateTimeDigitized", "unknown"));
            attributes.Add(exif.DateTimeOriginal != null
                ? CreateAtt("DateTimeOriginal", exif.DateTimeOriginal.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                : CreateAtt("DateTimeOriginal", "unknown"));
            if (!string.IsNullOrWhiteSpace(exif.Description)) attributes.Add(CreateAtt("Description", exif.Description.Trim()));
            if (!string.IsNullOrWhiteSpace(exif.Keyword)) attributes.Add(CreateAtt("Keyword", exif.Keyword.Trim()));
            if (string.IsNullOrWhiteSpace(exif.Title)) exif.Title = CurrnetFile.FileName;
            attributes.Add(CreateAtt("Title", exif.Title));
            if (!string.IsNullOrWhiteSpace(exif.RawFormat)) attributes.Add(CreateAtt("RawFormat", exif.RawFormat.Trim()));
            if (exif.GPS != null)
            {
                var gps = exif.GPS.ToString().Trim();
                if (gps != "-0.000000,-0.000000" && gps != "0.000000,0.000000")
                    attributes.Add(CreateAtt("GPS", gps));
            }
            if (!string.IsNullOrWhiteSpace(exif.Author)) attributes.Add(CreateAtt("Author", exif.Author.Trim()));
            if (!string.IsNullOrWhiteSpace(exif.Comment)) attributes.Add(CreateAtt("Comment", exif.Comment.Trim()));

            imageOrientation = exif.Orientation;
            if ((int)imageOrientation != 0)
                attributes.Add(CreateAtt("Orientation", exif.Orientation.ToString()));

            if (!string.IsNullOrWhiteSpace(exif.EquipmentModel)) attributes.Add(CreateAtt("EquipmentModel", exif.EquipmentModel.Trim()));
            if (!string.IsNullOrEmpty(exif.EquipmentMake))
            {
                attributes.Add(CreateAtt("EquipmentMake", exif.EquipmentMake));


            }
            if (image.Height > image.Width && ((double)image.Height / image.Width) < 0.5)
            {
                attributes.Add(CreateAtt("Panoramic", "1"));
                isPanoramic = true;
            }
            AspectRatioF f = AspectRatioF.FormSize(image.Size);
            attributes.Add(CreateAtt("AspectRatio", f.ToString()));
            foreach (var item in photo.Attributes)
            {
                item.AttValue = item.AttValue.Trim();
                if (item.AttValue != null && item.AttValue.Length > 255)
                {
                    item.AttValue = item.AttValue.Substring(0, 255);
                }
            }
        }

        private bool isPanoramic = false;

        private const int mbSize = 1024 * 1024;
    }
}