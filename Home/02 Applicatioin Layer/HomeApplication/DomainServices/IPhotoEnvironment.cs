using System;
using System.Linq;
using System.IO;
using Library.HelperUtility;
using Home.DomainModel.Repositories;
using Library.Storage;
using Library.Storage.Image;

namespace HomeApplication.DomainServices
{
    public interface IPhotoEnvironment
    {
        void LoadConfig(ISystemParameterRepository systemParRepository);

        //   string GalleryPath { get; }

        //  IImageFileStorage ImageStory { get; }
        bool Isloadconfig { get; }

        IImageStorage CreateImageStorage(Guid iD);
    }

    public class PhotoEnvironment : IPhotoEnvironment
    {
        public string GalleryPath { get; private set; }
        public bool Isloadconfig { get; private set; }

        public IImageStorage CreateImageStorage(Guid id)
        {
            return new ImageStorage(new PhysicalImageStorageProvider(GalleryPath), id);
        }

        public void LoadConfig(ISystemParameterRepository systemParRepository)
        {
            if (Isloadconfig) return;
            Isloadconfig = true;

            var configs = systemParRepository.GetListByGroup("GallerySetting");

            GalleryPath = configs.Cast<string>("GalleryPath", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GalleryPath"));
        }
    }
}