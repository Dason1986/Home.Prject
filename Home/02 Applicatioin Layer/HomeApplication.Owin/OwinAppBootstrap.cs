using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Library;
using Microsoft.Owin.Hosting;
using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HomeApplication
{
    public enum BootstrapMode
    {
        Dev,
        UAT,
        Production,
    }
    public class OwinAppBootstrap : Bootstrap
    {
        BootstrapMode _mode;
        public OwinAppBootstrap(IAppBuilder app, BootstrapMode mode)
        {
            _app = app;
            _mode = mode;
            _containerBuilder = new ContainerBuilder();
            Logger = LogManager.GetCurrentClassLogger();
        }

        protected IContainer _container;

        protected readonly ContainerBuilder _containerBuilder;
        protected ILogger Logger { get; set; }
        IAppBuilder _app;
        HttpConfiguration config = new HttpConfiguration();

        public static StartOptions CraeteStratOptions()
        {
            var settingPort = System.Configuration.ConfigurationManager.AppSettings["Port"];
            var port = Library.HelperUtility.StringUtility.TryCast(settingPort, 9000).Value;
            var option = new StartOptions("http://localhost:" + port)
            {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };
            option.Urls.Add(string.Format("http://{0}:{1}", "127.0.0.1", port));
            var hostname = System.Net.Dns.GetHostName();

            option.Urls.Add(string.Format("http://{0}:{1}", hostname, port));
            foreach (var address in System.Net.Dns.GetHostAddresses(hostname))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    option.Urls.Add(string.Format("http://{0}:{1}", address, port));
            }

            return option;
        }
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

        #region GetService

        public override T GetService<T>()
        {

            return _container.Resolve<T>();
        }
        public override T GetService<T>(Type[] type, object[] obj)
        {
            Parameter[] pars = new Parameter[type.Length];
            for (int i = 0; i < type.Length; i++)
            {
                pars[i] = new TypedParameter(type[i], obj[i]);
            }
            return _container.Resolve<T>(pars);
        }
        public override T GetService<T>(string[] constantNames, object[] obj)
        {
            Parameter[] pars = new Parameter[constantNames.Length];
            for (int i = 0; i < constantNames.Length; i++)
            {
                pars[i] = new NamedParameter(constantNames[i], obj[i]);
            }
            return _container.Resolve<T>(pars);
        }
        public override T GetService<T>(string name)
        {

            return _container.ResolveNamed<T>(name);
        }
        #endregion
    }

}
