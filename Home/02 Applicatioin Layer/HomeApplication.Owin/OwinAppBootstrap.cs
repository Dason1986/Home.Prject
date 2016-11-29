using Autofac;
using Autofac.Integration.WebApi;
using Library;
using Owin;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HomeApplication
{
    public class OwinAppBootstrap : ConsoleAppBootstrap
    {

        public OwinAppBootstrap(IAppBuilder app)
        {
            _app = app;
        }
        IAppBuilder _app;
        HttpConfiguration config = new HttpConfiguration();
        protected override void Register()
        {
            AutoMap.AutoMapProfile.Reg();

            Logger.Info("Ioc");
            _containerBuilder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());

            _containerBuilder.RegisterApiControllers(AppDomain.CurrentDomain.GetAssemblies());


            _container = _containerBuilder.Build();
            var resolver = new AutofacWebApiDependencyResolver(_container);
            config.DependencyResolver = resolver;
            WebConfig();
            Jobs.IOJobPlugin.Regter.RegJobs();
        }

        void WebConfig()
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);


            //     config.Routes.MapHttpRoute("Default", "{controller}/{action}", new { controller = "Home",action="Index" });
            //定义web api route
            config.Routes.MapHttpRoute(
    name: "ActionApi",
    routeTemplate: "api/{controller}/{action}/{id}",
    defaults: new { action="Get", id = RouteParameter.Optional }
);
       //     config.Routes.MapHttpRoute("API", "api/{controller}/{ID}", new { controller = "Home", ID = System.Web.Http.RouteParameter.Optional });

            //定义web api route
            //xml格式输出结果 
            // config.Formatters.XmlFormatter.UseXmlSerializer = false;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //     config.Formatters.Remove(config.Formatters.JsonFormatter);
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            //将web api以Middleware注册到OWIN管道中


            //    app.Use<HelloWorldMiddleware>();
            _app.UseWebApi(config);
        }

    }

}
