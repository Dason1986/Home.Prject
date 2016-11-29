using Autofac;
using Home.DomainModel.JobServices;
using HomeApplication.Jobs;

namespace HomeApplication
{
    public class TimerJobModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CopyFileService>().As<ICopyFileService>();
        }
    }
}