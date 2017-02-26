﻿using Home.DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using Home.DomainModel.Aggregates.GalleryAgg;
using System.Drawing;
using System.IO;
using Library.Infrastructure.Application;
using Home.DomainModel;
using Library.Draw;
using HomeApplication.ComponentModel.IO;

namespace HomeApplication.DomainServices
{
    public class AddPhotoDomainService : PhotoDomainService, IAddPhotoDomainService
    {
        public void Handle(Photo photo, Home.DomainModel.Aggregates.FileAgg.FileInfo file)
        {
            CurrnetPhoto = photo;
            CurrnetFile = file;
            DoAddAction();
        }

        private readonly IPhotoEnvironment _photoEnvironment = new PhotoEnvironment();
        Library.Storage.IFileStorage storage;
        Image image;
        Stream fs ;
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
            if (!_photoEnvironment.Isloadconfig) _photoEnvironment.LoadConfig(ModuleProvider.CreateSystemParameter());
            storage = CurrnetFile.GetStorage();
            Logger.Trace("AddPhotoDomainService:{0}", CurrnetFile.FullPath);


            if (!storage.Exists)
            {
                Logger.WarnByContent(Resources.DomainServiceResource.FileNotExist, CurrnetFile.FullPath);
                throw new PhotoDomainServiceException(Resources.DomainServiceResource.FileNotExist, new FileNotFoundException(CurrnetFile.FullPath));
            }
            if (CurrnetPhoto == null)
            {
                Logger.TraceByContent("Create Photo Entity", CurrnetFile.FullPath);

                CurrnetPhoto = new Photo(CreatedInfo.PhotoFileAnalysis)
                {
                    ID = CurrnetFile.ID,
                    FileID = CurrnetFile.ID,
                    PhotoType = PhotoType.Graphy,
                };
                PhotoRepository.Add(CurrnetPhoto);
                CurrnetFile.Photo = CurrnetPhoto;
            }
            if (CurrnetPhoto.Attributes != null && CurrnetPhoto.Attributes.Count > 0) return;

            Logger.TraceByContent("Analysis", CurrnetFile.FullPath);


            fs = storage.Get();
            image = new Bitmap(fs);


            var exifInfo = ImageExif.GetExifInfo(image);
            if (CurrnetPhoto.Attributes == null)
            {
                ICollection<PhotoAttribute> attributes = new List<PhotoAttribute>();
                CurrnetPhoto.Attributes = attributes;
            }
            DoImageExif(image, exifInfo);
            BuildImage(image);




        }

        private void BuildImage(Image image)
        {
            Logger.TraceByContent("Create Image", CurrnetFile.FullPath);
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
                PhotoID = CurrnetPhoto.ID,
                AttKey = key,
                AttValue = value
            };
            return additemExif;
        }


        protected void DoImageExif(Image image, Library.Draw.ImageExif exif)
        {
            Logger.TraceByContent("Create exif", CurrnetFile.FullPath);

            var photo = CurrnetPhoto;
            ICollection<PhotoAttribute> attributes = photo.Attributes;
            attributes.Add(exif.DateTimeDigitized != null
                ? CreateAtt("DateTimeDigitized", exif.DateTimeDigitized.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                : CreateAtt("DateTimeDigitized", "unknown"));
            attributes.Add(exif.DateTimeOriginal != null
                ? CreateAtt("DateTimeOriginal", exif.DateTimeOriginal.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                : CreateAtt("DateTimeOriginal", "unknown"));
            if (!string.IsNullOrWhiteSpace(exif.Description)) attributes.Add(CreateAtt("Description", exif.Description.Trim()));
            if (!string.IsNullOrWhiteSpace(exif.Keyword)) attributes.Add(CreateAtt("Keyword", exif.Keyword));
            if (string.IsNullOrWhiteSpace(exif.Title)) exif.Title = CurrnetFile.FileName;
            attributes.Add(CreateAtt("Title", exif.Title));
            if (!string.IsNullOrWhiteSpace(exif.RawFormat)) attributes.Add(CreateAtt("RawFormat", exif.RawFormat));
            if (exif.GPS != null) attributes.Add(CreateAtt("GPS", exif.GPS.ToString()));
            if (!string.IsNullOrWhiteSpace(exif.Author)) attributes.Add(CreateAtt("Author", exif.Author));
            if (!string.IsNullOrWhiteSpace(exif.Comment)) attributes.Add(CreateAtt("Comment", exif.Comment));

            imageOrientation = exif.Orientation;
            if ((int)imageOrientation != 0)
                attributes.Add(CreateAtt("Orientation", exif.Orientation.ToString()));

            if (!string.IsNullOrWhiteSpace(exif.EquipmentModel)) attributes.Add(CreateAtt("EquipmentModel", exif.EquipmentModel));
            if (!string.IsNullOrEmpty(exif.EquipmentMake))
            {
                attributes.Add(CreateAtt("EquipmentMake", exif.EquipmentMake));
                if (image.Height > image.Width && ((double)image.Height / image.Width) < 0.5)
                {
                    attributes.Add(CreateAtt("Panoramic", "1"));
                    isPanoramic = true;
                }

                attributes.Add(CreateAtt("AspectRatio", AspectRatio.FormSize(image.Size).ToString()));
            }
        }

        private bool isPanoramic = false;

        private const int mbSize = 1024 * 1024;
    }
}