using DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.DomainEvents;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;

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
           var faces= _photoFacesRepository.GetFacesByPhtotID(CurrnetPhoto.ID);
            if (faces != null || faces.Count > 0) return;
        }

       
    }
}
