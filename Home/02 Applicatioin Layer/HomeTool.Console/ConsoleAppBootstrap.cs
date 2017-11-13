using Autofac;
using Autofac.Core;
using Library;
using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HomeApplication
{
	
    public class ConsoleAppBootstrap : Bootstrap
    {

        public ConsoleAppBootstrap()
        {

            _containerBuilder = new ContainerBuilder();
            Logger = LogManager.GetCurrentClassLogger();
        }

        protected IContainer _container;

        protected readonly ContainerBuilder _containerBuilder;
        protected ILogger Logger { get; set; }



        protected override void Register()
        {
        
            AutoMap.AutoMapProfile.Reg();

           
            Logger.Info("Ioc");
            Logger.Info(" injection db");
            _containerBuilder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());

            /*
            _containerBuilder.RegisterModule<RepositoryModule>();

            Logger.Info(" injection DomainService");

            _containerBuilder.RegisterModule<DomainServiceModule>();


            Logger.Info(" injection Jobs");

            _containerBuilder.RegisterModule<TimerJobModule>();
            */
         
            _container = _containerBuilder.Build();
          //  Jobs.IOJobPlugin.Regter.RegJobs();
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

        public override object GetService(Type type)
        {
            throw new NotImplementedException();
        }

        public override object GetService(Type type, Type[] argtypes, object[] obj)
        {
            throw new NotImplementedException();
        }

        public override object GetService(Type type, string[] constantNames, object[] obj)
        {
            throw new NotImplementedException();
        }
    }


}
