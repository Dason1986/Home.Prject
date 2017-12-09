using Autofac;
using Module = Autofac.Module;
using HomeApplication.DomainServices;
using HomeApplication.Interceptors;
using HomeApplication.Jobs;
using HomeApplication.Cores;

namespace HomeApplication
{
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