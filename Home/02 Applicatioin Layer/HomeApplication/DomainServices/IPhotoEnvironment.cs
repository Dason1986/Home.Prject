using System;
using System.Linq;
using System.IO;
using Library.HelperUtility;
using Home.DomainModel.Repositories;
using Library.IO.Storage.Image;
using Library.IO.Storage;

namespace HomeApplication.DomainServices
{

    public interface IPhotoEnvironment
    {
        void LoadConfig(ISystemParameterRepository systemParRepository);
        string GalleryPath { get; }
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
            var path = FileStoryHelper.InitPath(GalleryPath, id);
            return new ImageStorage(id, new PhysicalImageStorageProvider(path));
        }

        public void LoadConfig(ISystemParameterRepository systemParRepository)
        {
            if (Isloadconfig) return;
            Isloadconfig = true;

            var configs = systemParRepository.GetAll().Where(n => n.Group == "GallerySetting").ToList();
            //    ImageStory = new ImageFileStorage();
            GalleryPath = configs.FirstOrDefault(n => n.Key == "GalleryPath").GetValue(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GalleryPath"));
            //     EffectImagePath = FileStoryHelper.InitPath(configs.FirstOrDefault(n => n.Key == "EffectImagePath").GetValue(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EffectImagePath")));


        }
    }
}
