using Autofac;
using HomeApplication.Services;

namespace HomeApplication
{
    public class APIServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalleryServiceImpl>().As<IGalleryService>();
            builder.RegisterType<PMSServiceImpl>().As<IPMSService>();
            builder.RegisterType<FileManagementServiceImpl>().As<FileManagementServiceImpl>();
        }
    }
}