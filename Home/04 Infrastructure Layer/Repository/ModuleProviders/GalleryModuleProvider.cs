using Home.DomainModel.ModuleProviders;
using Library.Domain.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Home.DomainModel.Repositories;
using Home.Repository.Repositories;

namespace Home.Repository.ModuleProviders
{
    public class GalleryModuleProvider : DomainModuleProvider, IGalleryModuleProvider
    {
        public GalleryModuleProvider(EFContext context) : base(context)
        {
        }

        public IAlbumRepository CreateAlbum()
        {
            return new AlbumRepository(this.Context);
        }

        public IFileInfoRepository CreateFileInfo()
        {
            return new FileInfoRepository(this.Context);
        }

        public IPhotoRepository CreatePhoto()
        {
            return new PhotoRepository(this.Context);
        }

        public IPhotoAttributeRepository CreatePhotoAttribute()
        {
            return new PhotoAttributeRepository(this.Context);
        }

        public IPhotoFacesRepository CreatePhotoFaces()
        {
            return new PhotoFacesRepository(this.Context);
        }

        public IPhotoFingerprintRepository CreatePhotoFingerprint()
        {
            return new PhotoFingerprintRepository(this.Context);
        }

        public IPhotoSimilarRepository CreatePhotoSimilar()
        {
            return new PhotoSimilarRepository(this.Context);
        }

        public ISystemParameterRepository CreateSystemParameter()
        {
            return new SystemParameterRepository(this.Context);
        }
    }
}