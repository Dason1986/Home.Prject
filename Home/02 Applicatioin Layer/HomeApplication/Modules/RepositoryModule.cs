using Autofac;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.Data.EF;
using Repository;
using Home.Repository.ModuleProviders;
using Home.Repository.Repositories;
using Module = Autofac.Module;
using System.Collections.Generic;
using System;
using Library.Domain.Data.Repositorys;
using Library.Domain.Data.ModuleProviders;

namespace HomeApplication
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainBoundedContext>().As<MainBoundedContext, System.Data.Entity.DbContext, IDbContext>();
            FindInterfaceMappToType find = new FindInterfaceMappToType(typeof(IRepository), typeof(IAlbumRepository).Assembly, typeof(MainBoundedContext).Assembly);
            var mappdic = find.Find();
            foreach (var typese in mappdic)
            {
                if (typese.Value.Length > 0)
                    builder.RegisterType(typese.Value[0]).As(typese.Key);
            }
            find = new FindInterfaceMappToType(typeof(IModuleProvider), typeof(IGalleryModuleProvider).Assembly, typeof(MainBoundedContext).Assembly);
            mappdic = find.Find();
            foreach (var typese in mappdic)
            {
                builder.RegisterType(typese.Value[0]).As(typese.Key);
            }
            // builder.RegisterType(typeof(AlbumRepository)).As(typeof(IAlbumRepository));
            //builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            //builder.RegisterType<FileInfoRepository>().As<IFileInfoRepository>();
            //builder.RegisterType<PhotoAttributeRepository>().As<IPhotoAttributeRepository>();
            //builder.RegisterType<PhotoFingerprintRepository>().As<IPhotoFingerprintRepository>();
            //builder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
            //builder.RegisterType<SystemParameterRepository>().As<ISystemParameterRepository>();
            //builder.RegisterType<PhotoSimilarRepository>().As<IPhotoSimilarRepository>();
            //builder.RegisterType<StorageEngineRepository>().As<IStorageEngineRepository>();

            //builder.RegisterType<FileManagentModuleProvider>().As<IFileManagentModuleProvider>();
            //builder.RegisterType<GalleryModuleProvider>().As<IGalleryModuleProvider>();
        }
    }
}