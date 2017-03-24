using Home.DomainModel.Repositories;
using Library.Domain.Data;

namespace Home.DomainModel.ModuleProviders
{
    public interface IGalleryModuleProvider : IDomainModuleProvider
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