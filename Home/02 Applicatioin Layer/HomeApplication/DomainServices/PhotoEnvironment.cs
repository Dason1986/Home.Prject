using System;
using System.Linq;
using System.IO;
using Library.HelperUtility;
using Home.DomainModel.Repositories;

namespace HomeApplication.DomainServices
{

    public class PhotoEnvironment : IPhotoEnvironment
    {
        public IFileProvider ThumbnailPath { get; private set; }

        public IFileProvider EffectImagePath { get; private set; }
        public bool IsLoadconfig { get; private set; }
        public void LoadConfig(ISystemParameterRepository systemParRepository)
        {
            if (IsLoadconfig) return;
            IsLoadconfig = true;

            var configs = systemParRepository.GetAll().Where(n => n.Group == "GallerySetting").ToList();
            ThumbnailPath = new PhysicalFileProvider(configs.FirstOrDefault(n => n.Key == "ThumbnailPath").GetValue(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ThumbnailPath")));
            EffectImagePath = new PhysicalFileProvider(configs.FirstOrDefault(n => n.Key == "EffectImagePath").GetValue(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EffectImagePath")));


        }
    }
}