using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Home.DomainModel.Repositories;
using HomeApplication.Interceptors;
using HomeApplication.Jobs;
using HomeApplication.Owin;
using HomeApplication.Owin.Core;
using Library;
using Library.ExceptionProviders;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
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
            InitErrorProvider();
            WebConfig();
            AutoMap.AutoMapProfile.Reg();

            Logger.Info("Ioc");
            _containerBuilder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());

            _containerBuilder.RegisterApiControllers(AppDomain.CurrentDomain.GetAssemblies());


            _container = _containerBuilder.Build();

            var resolver = new AutofacWebApiDependencyResolver(_container);
            config.DependencyResolver = resolver;

            ScheduleJobManagement jobManagement = GetService<ScheduleJobManagement>();
            jobManagement.LoadProvider();
            jobManagement.Run();

            var serialNumberBuilder = GetService<SerialNumberBuilderProvider>();
            serialNumberBuilder.Initialize();

        }
        private void InitErrorProvider()
        {
            Logger.Trace("註冊錯誤信息處理器");
            CustomExceptionProvider.Add(new LogicFaultExceptionProvider());
         //   CustomExceptionProvider.Add(new NotImplementedExceptionProvider());
       //     CustomExceptionProvider.Add(new ArgumentExceptionProvider());
            CustomExceptionProvider.Add(new DbValidataionFaultExceptionProvider());
            CustomExceptionProvider.Add(new EFUpdateFaultExceptionProvider());
            CustomExceptionProvider.Add(new EFDbUpdateFaultExceptionProvider());
        }
        private void DefaultJsonFormat(HttpConfiguration config)
        {
            Logger.Trace("設置JsonFormat");
            var formatters = config.Formatters;
            if (formatters.XmlFormatter != null) config.Formatters.Remove(formatters.XmlFormatter);
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
            settings.Formatting = Formatting.Indented;
            settings.FloatParseHandling = FloatParseHandling.Decimal;

            // settings.Converters.Add(new DatenullJsonConverter());
            settings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//CamelCasePropertyNamesContractResolver();

            //    config.Formatters.Insert(0, new JsonpMediaTypeFormatter(jsonFormatter));
            // logger.Info("日期格式：{0}", settings.DateFormatString);
        }
        void WebConfig()
        {
            SetWebApi();
            DefaultJsonFormat(config);

            //将web api以Middleware注册到OWIN管道中


            //    app.Use<HelloWorldMiddleware>();
            Logger.Trace("啟用:Swagger");
            config.RegisterSwagger();
            _app.UseWebApi(config);
        }

        private void SetWebApi()
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();
            //定义web api route
            config.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "api/{controller}/{id}",
                        defaults: new { id = RouteParameter.Optional }
                    );
            config.Filters.Add(new LogicExceptionFilterAttribute());
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

        public override object GetService(Type type)
        {
            return _container.Resolve(type);
        }

        public override object GetService(Type type, Type[] argtypes, object[] obj)
        {
            Parameter[] pars = new Parameter[argtypes.Length];
            for (int i = 0; i < argtypes.Length; i++)
            {
                pars[i] = new TypedParameter(argtypes[i], obj[i]);
            }
            return _container.Resolve(type, pars);
        }
        public override object GetService(Type type, string[] constantNames, object[] obj)
        {
            Parameter[] pars = new Parameter[constantNames.Length];
            for (int i = 0; i < constantNames.Length; i++)
            {
                pars[i] = new NamedParameter(constantNames[i], obj[i]);
            }
            return _container.Resolve(type, pars);
        }
        #endregion
    }

}
