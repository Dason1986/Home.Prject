using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Collections.Generic;
using System.Reflection;

[assembly: OwinStartup("ProductionConfiguration", typeof(HomeApplication.Owin.StartupProduction))]
[assembly: OwinStartup("DevConfiguration", typeof(HomeApplication.Owin.StartupDev))]
[assembly: OwinStartup("UATConfiguration", typeof(HomeApplication.Owin.StartupUAT))]

namespace HomeApplication.Owin
{

    public class StartupProduction
    {
        public void Configuration(IAppBuilder app)
        {

            OwinAppBootstrap boot = new OwinAppBootstrap(app, BootstrapMode.Production);

            boot.Run();

        }
    }
    public class StartupUAT
    {
        public void Configuration(IAppBuilder app)
        {

            OwinAppBootstrap boot = new OwinAppBootstrap(app, BootstrapMode.UAT);

            boot.Run();

        }
    }
    public class StartupDev
    {
        public void Configuration(IAppBuilder app)
        {

            OwinAppBootstrap boot = new OwinAppBootstrap(app, BootstrapMode.Dev);

            boot.Run();

        }
    }
}
