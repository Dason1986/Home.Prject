using Home.DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.ModuleProviders;
using Library.Domain.DomainEvents;
using Home.DomainModel.Repositories;
using Home.DomainModel;
using System.Drawing;
using Library;

namespace HomeApplication.DomainServices
{
    public class BuildFingerprintDomainService : PhotoDomainService, IBuildFingerprintDomainService
    {
        public BuildFingerprintDomainService()
        {
            SetAlgorithm(SimilarAlgorithm.PerceptualHash);

        }
        private IPhotoFingerprintRepository photoFingerprintRepository;
        Library.Draw.SimilarImages.SimilarAlgorithm _grayHistogram;

        public SimilarAlgorithm Algorithm { get; private set; }
        public void SetAlgorithm(SimilarAlgorithm type)
        {
            Algorithm = type;
            switch (Algorithm)
            {
                case SimilarAlgorithm.GrayHistogram:
                    _grayHistogram = new Library.Draw.SimilarImages.GrayHistogram();
                    break;
                case SimilarAlgorithm.PerceptualHash:
                    _grayHistogram = new Library.Draw.SimilarImages.PerceptualHash();
                    break;
                default:
                    break;
            }
        }

         

        public void Handle(Photo photo)
        {
            if (photo == null) return;


            CurrnetPhoto = photo;
            CurrnetFile = CurrnetPhoto.File;
            DoAddAction();
        //    ModuleProvider.UnitOfWork.Commit();

        }
        protected override void CreateRepository(IGalleryModuleProvider moduleProvider)
        {
            base.CreateRepository(moduleProvider);
            photoFingerprintRepository = moduleProvider.CreatePhotoFingerprint();
        }

        protected override void DoAddAction()
        {
            Photo photo = CurrnetPhoto;
            if (photoFingerprintRepository.Exist(photo.ID, Algorithm)) return;
            Logger.Trace(CurrnetFile.FileName);
            #region MyRegion
            Image image = null;
            try
            {
                image = Image.FromFile(CurrnetFile.FullPath);
                if (image.Width < 256) return;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex);
                Logger.Error(ex, CurrnetFile.FullPath + "文件不能以图像形式打开！");
                return;

            }

            var fingerprint = _grayHistogram.BuildFingerprint(image);
            var photoFingerprint = new PhotoFingerprint(CreatedInfo.BuildFingerprint)
            {
                Algorithm = SimilarAlgorithm.PerceptualHash,
                PhotoID = photo.ID,
                //    Owner = item,
                Fingerprint = fingerprint
            };
            photoFingerprintRepository.Add(photoFingerprint);

            image.Dispose();
            #endregion
        }

    }
}
