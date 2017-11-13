using Home.DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.Data.ModuleProviders;

namespace Home.DomainModel.ModuleProviders
{
    public interface IGalleryModuleProvider : IModuleProvider
    {
        IPhotoRepository CreatePhoto();

        IAlbumRepository CreateAlbum();

        IPhotoAttributeRepository CreatePhotoAttribute();

        IFileInfoRepository CreateFileInfo();

        IPhotoFingerprintRepository CreatePhotoFingerprint();

        IPhotoSimilarRepository CreatePhotoSimilar();

        IPhotoFacesRepository CreatePhotoFaces();

        ISystemParameterRepository CreateSystemParameter();
    }
}