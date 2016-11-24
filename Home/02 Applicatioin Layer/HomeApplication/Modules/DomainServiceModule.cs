using Autofac;
using DomainModel.DomainServices;
using HomeApplication.DomainServices;
using HomeApplication.Services;
using Library.Domain.Data;
using Library.Domain.Data.EF;
using Repository;

namespace HomeApplication
{
    public class DomainServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<MainBoundedContext>().As<EFContext, IDbContext>();

            builder.RegisterType<AddPhotoDomainService>().As<IAddPhotoDomainService>();
            builder.RegisterType<BuildFingerprintDomainService>().As<IBuildFingerprintDomainService>();
            builder.RegisterType<SimilarPhotoDomainService>().As<ISimilarPhotoDomainService>();
            builder.RegisterType<PhotoFacesDomainService>().As<IPhotoFacesDomainService>();

        }
    }
    public class APIServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<GalleryServiceImpl>().As<IGalleryService>();


        }
    }
}