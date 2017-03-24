using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.Data.EF;
using Repository;
using Home.Repository.ModuleProviders;
using Home.Repository.Repositories;
using Module = Autofac.Module;

namespace HomeApplication
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainBoundedContext>().As<EFContext, IDbContext>();
            FindInterfaceMappToType find = new FindInterfaceMappToType(typeof(IRepository), typeof(IAlbumRepository).Assembly, typeof(MainBoundedContext).Assembly);
            var mappdic = find.Find();
            foreach (var typese in mappdic)
            {
                builder.RegisterType(typese.Value[0]).As(typese.Key);
            }
            find = new FindInterfaceMappToType(typeof(IDomainModuleProvider), typeof(IGalleryModuleProvider).Assembly, typeof(MainBoundedContext).Assembly);
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

    public class FindInterfaceMappToType
    {
        private readonly Type _interfacType;
        private readonly Assembly _intefaceAssembly;
        private readonly Assembly _maptoAssembly;

        public FindInterfaceMappToType(Type interfacType, Assembly intefaceAssembly, Assembly maptoAssembly)
        {
            _interfacType = interfacType;
            _intefaceAssembly = intefaceAssembly;
            _maptoAssembly = maptoAssembly;
        }

        public IDictionary<Type, Type[]> Find()
        {
            IDictionary<Type, Type[]> dictionary = new Dictionary<Type, Type[]>();
            var types =
              _intefaceAssembly.GetTypes()
                  .Where(n => _interfacType.IsAssignableFrom(n) && n.IsInterface && n != _interfacType).OrderBy(n => n.Name)
                  .ToArray();
            var mapptos =
                _maptoAssembly.GetTypes()
                    .Where(
                        n => _interfacType.IsAssignableFrom(n) && n.IsClass && !n.IsAbstract && n != _interfacType)
                    .ToArray();
            foreach (var type in types)
            {
                if (type.IsGenericType) continue;

                var maptoType = mapptos.Where(n => type.IsAssignableFrom(n) && n.IsClass && !n.IsAbstract).ToArray();

                dictionary.Add(type, maptoType.Length == 0 ? null : maptoType);
            }
            return dictionary;
        }
    }
}