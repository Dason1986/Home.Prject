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
			
			// 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
			OwinAppBootstrap bootstrap = new OwinAppBootstrap();
			 
			bootstrap.Run();
            var config = GlobalConfiguration.Configuration;

            //     config.Routes.MapHttpRoute("Default", "{controller}/{action}", new { controller = "Home",action="Index" });//定义web api route
            config.Routes.MapHttpRoute("API", "api/{controller}/{ID}", new { controller = "Home", ID = System.Web.Http.RouteParameter.Optional });//定义web api route
                                                                                                                                                   //xml格式输出结果 
                                                                                                                                                      // config.Formatters.XmlFormatter.UseXmlSerializer = false;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
       //     config.Formatters.Remove(config.Formatters.JsonFormatter);
             config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            //将web api以Middleware注册到OWIN管道中

    
        //    app.Use<HelloWorldMiddleware>();
            app.UseWebApi(config);
        }
    }
   
}
