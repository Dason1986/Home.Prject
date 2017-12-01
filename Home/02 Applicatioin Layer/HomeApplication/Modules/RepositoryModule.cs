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
using HomeApplication.DomainServices;
using HomeApplication.Interceptors;
using HomeApplication.Jobs;
using HomeApplication.Cores;

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
                {
                    builder.RegisterType(typese.Value[0])
                        .As(typese.Key);
                     
                }
            }
            find = new FindInterfaceMappToType(typeof(IModuleProvider), typeof(IGalleryModuleProvider).Assembly, typeof(MainBoundedContext).Assembly);
            mappdic = find.Find();
            foreach (var typese in mappdic)
            {
                builder.RegisterType(typese.Value[0]).As(typese.Key);
            }
        }
    }

    public class ApplictionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PhotoEnvironment>()
                .As<IPhotoEnvironment>()
                .As<PhotoEnvironment>()
                .AsImplementedInterfaces()
               .InstancePerRequest()
                .SingleInstance();

            builder.RegisterType<ScheduleJobManagement>()
           .As<ScheduleJobManagement>()
           .AsImplementedInterfaces()
           .InstancePerRequest()
           .SingleInstance();

            builder.RegisterType<HomeSerialNumberBuilderProvider>()
                .As<ISerialNumberBuilderProvider>()
                .As<SerialNumberBuilderProvider>()
           .AsImplementedInterfaces()
           .InstancePerRequest()
                .SingleInstance();

        }
    }
}