using System.Collections.Generic;

using HomeApplication.Dtos;

namespace HomeApplication.Services
{
    [System.ServiceModel.ServiceContract]
    public interface IGalleryService
    {
        [System.ServiceModel.OperationContract]
        IList<AlbumDto> GetAlbums();
        [System.ServiceModel.OperationContract]
        int GetAllPhotoTotal();
        [System.ServiceModel.OperationContract]
        int GetPhotoTotalByGallery();
    }
}