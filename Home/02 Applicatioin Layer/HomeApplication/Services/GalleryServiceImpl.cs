using DomainModel.Repositories;

namespace HomeApplication.Services
{

    public class GalleryServiceImpl : ServiceImpl
    {
        public override string ServiceName { get { return "Gallery Service"; } }

        public int GetAllPhotoTotal()
        {
            IPhotoRepository photoRepository = Library.Bootstrap.Currnet.GetService<IPhotoRepository>();
            return photoRepository.GetAllPhotoTotal();
        }

        public int GetPhotoTotalByGallery()
        {
            IPhotoRepository photoRepository = Library.Bootstrap.Currnet.GetService<IPhotoRepository>();
            return photoRepository.GetAllPhotoTotal();
        }

    }
}