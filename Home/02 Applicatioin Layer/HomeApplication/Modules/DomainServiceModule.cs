using Autofac;
using Autofac.Extras.DynamicProxy;
using Home.DomainModel.DomainServices;
using HomeApplication.DomainServices;
using Library.Domain.Data;
using Library.Domain.Data.EF;
using Repository;

namespace HomeApplication
{
    public class DomainServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
          

            builder.RegisterType<AddPhotoDomainService>().As<IAddPhotoDomainService>().EnableInterfaceInterceptors();
            builder.RegisterType<BuildFingerprintDomainService>().As<IBuildFingerprintDomainService>().EnableInterfaceInterceptors();
            builder.RegisterType<SimilarPhotoDomainService>().As<ISimilarPhotoDomainService>().EnableInterfaceInterceptors();
            builder.RegisterType<PhotoFacesDomainService>().As<IPhotoFacesDomainService>().EnableInterfaceInterceptors();
            builder.RegisterType<AddFileDomainService>().As<IAddFileDomainService>().EnableInterfaceInterceptors();
        }
    }
}