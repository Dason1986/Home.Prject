using Home.DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.DomainEvents;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Accord.Vision.Detection.Cascades;
using Accord.Vision.Detection;
using System.Drawing;
using System.IO;
using Library.Infrastructure.Application;
using Accord.Imaging.Filters;

namespace HomeApplication.DomainServices
{
    public class PhotoFacesDomainService : PhotoDomainService, IPhotoFacesDomainService
    {
        IPhotoFacesRepository _photoFacesRepository;

        protected override void CreateRepository(IGalleryModuleProvider moduleProvider)
        {
            base.CreateRepository(moduleProvider);
            _photoFacesRepository = moduleProvider.CreatePhotoFaces();

        }
        protected override void DoAddAction()
        {
            var faces = _photoFacesRepository.GetFacesByPhtotID(CurrnetPhoto.ID);
            if (faces != null || faces.Count > 0) return;
            Bitmap image;

            Stream fs = null;
            try
            {
                fs = System.IO.File.Open(CurrnetFile.FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                image = new Bitmap(fs);
            }
            catch (Exception ex)
            {

                Logger.ErrorByContent(ex, "File can not open！", CurrnetFile.FullPath);
                return;

            }
            HaarCascade cascade = new FaceHaarCascade();
            var detector = new HaarObjectDetector(cascade, 30);

            detector.SearchMode = ObjectDetectorSearchMode.NoOverlap;
            detector.ScalingMode = ObjectDetectorScalingMode.SmallerToGreater;
            detector.ScalingFactor = 1.5f;
            detector.UseParallelProcessing = true;
            detector.Suppression = 2;
            Rectangle[] objects = detector.ProcessFrame(image);
            RectanglesMarker marker = new RectanglesMarker(objects, Color.Fuchsia);
          
        }


    }
}
