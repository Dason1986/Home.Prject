using Autofac;
using HomeApplication.Interceptors;
using Library.Domain.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Modules
{
   public   class GenericModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterGeneric(typeof(List<>)).As(typeof(IList<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Library.Domain.Data.EF.Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<RoleInterceptor>();
        }
    }
}
