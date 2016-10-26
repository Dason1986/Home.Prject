using Autofac;
using Autofac.Core;
using DomainModel.DomainServices;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using HomeApplication.DomainServices;
using Library;
using Library.Domain.Data;
using Library.Domain.Data.EF;
using Library.Domain.DomainEvents;
using NLog;
using Repository;
using Repository.ModuleProviders;
using Repository.Repositories;
using System;

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
            _containerBuilder.RegisterType<MainBoundedContext>().As<EFContext, IDbContext>();

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
            //      _containerBuilder.RegisterType<DomainEventBus>().As<IDomainEventBus>();
            _containerBuilder.RegisterType<AddPhotoDomainService>().As<IAddPhotoDomainService>();
            _containerBuilder.RegisterType<BuildFingerprintDomainService>().As<IBuildFingerprintDomainService>();
            _containerBuilder.RegisterType<SimilarPhotoDomainService>().As<ISimilarPhotoDomainService>();
            _containerBuilder.RegisterType<PhotoFacesDomainService>().As<IPhotoFacesDomainService>();
            //   _containerBuilder.RegisterType<DomainEventBus>().Named<IDomainEventBus>("DomainEventBus").As<IDomainEventBus>();

            Logger.Info(" 注入 Jobs");


            _container = _containerBuilder.Build();
        }
        public override T GetService<T>()
        {

            return _container.Resolve<T>();
        }
        public override  T GetService<T>(Type[] type, object[] obj)
        {
            Parameter[] pars = new Parameter[type.Length];
            for (int i = 0; i < type.Length; i++)
            {
                pars[i] = new TypedParameter(type[i], obj[i]);
            }
            return _container.Resolve<T>(pars);
        }
        public override  T GetService<T>(string[] constantNames, object[] obj)
        {
            Parameter[] pars = new Parameter[constantNames.Length];
            for (int i = 0; i < constantNames.Length; i++)
            {
                pars[i] = new NamedParameter(constantNames[i], obj[i]);
            }
            return _container.Resolve<T>(pars);
        }
        public override T GetService<T>(string name)
        {

            return _container.ResolveNamed<T>(name);
        }

    }


}
