using Autofac;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using Library;
using Library.Domain.Data;
using Library.Domain.Data.EF;
using Library.Domain.DomainEvents;
using NLog;
using Repository;
using Repository.ModuleProviders;
using Repository.Repositories;

namespace HomeApplication
{
    public class ConsoleAppBootstrap : Bootstrap
    {

        public ConsoleAppBootstrap() : base()
        {
            _containerBuilder = new Autofac.ContainerBuilder();
            Logger = LogManager.GetCurrentClassLogger();
        }

        Autofac.IContainer _container;

        Autofac.ContainerBuilder _containerBuilder;
        protected NLog.ILogger Logger { get; set; }



        protected override void Register()
        {
            Logger.Info("Ioc 注入");
            Logger.Info(" 注入 db");
            _containerBuilder.RegisterType<MainBoundedContext>().As<EFContext>();

            Logger.Info(" 注入 Repository");
            _containerBuilder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            _containerBuilder.RegisterType<FileInfoRepository>().As<IFileInfoRepository>();
            _containerBuilder.RegisterType<PhotoAttributeRepository>().As<IPhotoAttributeRepository>();
            _containerBuilder.RegisterType<PhotoFingerprintRepository>().As<IPhotoFingerprintRepository>();
            _containerBuilder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
            _containerBuilder.RegisterType<PhotoSimilarRepository>().As<IPhotoSimilarRepository>();

            Logger.Info(" 注入 ModuleProvider");
            _containerBuilder.RegisterType<GalleryModuleProvider>().As<IGalleryModuleProvider>();


            Logger.Info(" 注入 DomainService");
            _containerBuilder.RegisterType<DomainEventBus>().As<IDomainEventBus>();


            Logger.Info(" 注入 Jobs");

          
            _container = _containerBuilder.Build();
        }
        public override T GetService<T>()
        {

            return _container.Resolve<T>();
        }


    }
}
