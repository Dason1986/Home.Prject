using Home.DomainModel.ModuleProviders;
using Library.Domain.Data.EF;
using Home.DomainModel.Repositories;
using Home.Repository.Repositories;
using System.Data.Entity;

namespace Home.Repository.ModuleProviders
{
    public class GalleryModuleProvider : ModuleProvider, IGalleryModuleProvider
    {
        public GalleryModuleProvider(DbContext context) : base(context)
        {
        }

        public IAlbumRepository CreateAlbum()
        {
            return new AlbumRepository(this.DbContext);
        }

        public IFileInfoRepository CreateFileInfo()
        {
            return new FileInfoRepository(this.DbContext);
        }

        public IPhotoRepository CreatePhoto()
        {
            return new PhotoRepository(this.DbContext);
        }

        public IPhotoAttributeRepository CreatePhotoAttribute()
        {
            return new PhotoAttributeRepository(this.DbContext);
        }

        public IPhotoFacesRepository CreatePhotoFaces()
        {
            return new PhotoFacesRepository(this.DbContext);
        }

        public IPhotoFingerprintRepository CreatePhotoFingerprint()
        {
            return new PhotoFingerprintRepository(this.DbContext);
        }

        public IPhotoSimilarRepository CreatePhotoSimilar()
        {
            return new PhotoSimilarRepository(this.DbContext);
        }

        public ISystemParameterRepository CreateSystemParameter()
        {
            return new SystemParameterRepository(this.DbContext);
        }
    }
}