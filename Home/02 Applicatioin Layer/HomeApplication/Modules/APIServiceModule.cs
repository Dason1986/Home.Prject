using Autofac;
using HomeApplication.Services;

namespace HomeApplication
{
    public class APIServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<GalleryServiceImpl>().As<IGalleryService>();


        }
    }
}