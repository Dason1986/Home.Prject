using Autofac;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.Data.EF;
using Repository;
using Repository.ModuleProviders;
using Repository.Repositories;

namespace HomeApplication
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<MainBoundedContext>().As<EFContext, IDbContext>();


            builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            builder.RegisterType<FileInfoRepository>().As<IFileInfoRepository>();
            builder.RegisterType<PhotoAttributeRepository>().As<IPhotoAttributeRepository>();
            builder.RegisterType<PhotoFingerprintRepository>().As<IPhotoFingerprintRepository>();
            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
			builder.RegisterType<SystemParameterRepository>().As<ISystemParameterRepository>();
			builder.RegisterType<PhotoSimilarRepository>().As<IPhotoSimilarRepository>();

            builder.RegisterType<GalleryModuleProvider>().As<IGalleryModuleProvider>();

        }
    }
}