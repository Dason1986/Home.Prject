using Autofac;
using Autofac.Core;
using Library;
using NLog;
using System;

namespace HomeApplication
{
    public class ConsoleAppBootstrap : Bootstrap
    {

        public ConsoleAppBootstrap() : base()
        {

            _containerBuilder = new Autofac.ContainerBuilder();
            Logger = LogManager.GetCurrentClassLogger();
        }

        Autofac.IContainer _container;

        Autofac.ContainerBuilder _containerBuilder;
        protected NLog.ILogger Logger { get; set; }



        protected override void Register()
        {
            AutoMap.AutoMapProfile.Reg();

            Logger.Info("Ioc 注入");
            Logger.Info(" 注入 db");
            _containerBuilder.RegisterModule<RepositoryModule>();

            Logger.Info(" 注入 DomainService");

            _containerBuilder.RegisterModule<DomainServiceModule>();


            Logger.Info(" 注入 Jobs");

            _containerBuilder.RegisterModule<TimerJobModule>();

            _container = _containerBuilder.Build();
        }
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

    }


}
