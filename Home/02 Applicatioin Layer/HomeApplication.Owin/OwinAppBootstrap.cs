using Autofac;
using Autofac.Integration.WebApi;
using Library;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace HomeApplication
{
	public class OwinAppBootstrap : ConsoleAppBootstrap
	{

	 

	 
		protected override void Register()
		{
			AutoMap.AutoMapProfile.Reg();

			Logger.Info("Ioc");
			Logger.Info(" injection db");
            _containerBuilder.RegisterApiControllers(AppDomain.CurrentDomain.GetAssemblies());

            _containerBuilder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());
		
			_container = _containerBuilder.Build();
		}

		 

	}

}
