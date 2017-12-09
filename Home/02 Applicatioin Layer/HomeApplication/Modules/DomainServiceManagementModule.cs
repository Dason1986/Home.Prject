using Autofac;
using Module = Autofac.Module;
using Library.Domain.DomainEvents;
using HomeApplication.Jobs;
using Home.DomainModel.DomainServices;

namespace HomeApplication
{
    public class DomainServiceManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var interfacetype = typeof(IDomainService);
            var assembly = typeof(DomainServiceManagementModule).Assembly;
            var formassembly = typeof(IAddFileDomainService).Assembly;
            var Mapping = new FindInterfaceMappToType(interfacetype, formassembly, assembly).Find();



            foreach (var type in Mapping)
            {
                if (type.Value != null)
                {
                    if (type.Value.Length == 1)
                    {
                        var domaintype = type.Value[0];
                        builder.RegisterType(domaintype).As(domaintype).As(type.Key).SingleInstance();
                        if (domaintype.BaseType.IsGenericType)
                        {


                            var arguments = domaintype.BaseType.GetGenericArguments();
                            if (arguments.Length > 0)
                            {
                                DomainServiceManagement.EventMapping.Add(arguments[0], domaintype);
                            }
                        }
                    }

                }
            }
        }

    }
}