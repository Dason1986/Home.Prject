using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Collections.Generic;
using System.Reflection;

[assembly: OwinStartup(typeof(HomeApplication.Owin.Startup1))]

namespace HomeApplication.Owin
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            OwinAppBootstrap boot = new OwinAppBootstrap(app);
           
            boot.Run();
        
        }
    }
   
}
