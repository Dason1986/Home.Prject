using Library.Domain.DomainEvents;
using System;
using Library.Domain.Data;
using System.Collections.Generic;
using Library;
using Library.Domain.Data.ModuleProviders;

namespace HomeApplication.Jobs
{
    public abstract class TDomainService<TEvents> : IDomainService where TEvents : DomainEventArgs
    {
        public IModuleProvider ModuleProvider { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Invoke(TEvents @event)
        {
            OnInvoke(@event);
        }
        protected abstract void OnInvoke(TEvents @event);

        void IDomainService.Handle(DomainEventArgs @event)
        {
            this.Invoke((TEvents)@event);
        }
    }

    public sealed class DomainServiceManagement
    {




        internal static IDictionary<Type, Type> EventMapping = new Dictionary<Type, Type>();
        public IDomainService GetDomainService(Type domainServicType)
        {
            if (!EventMapping.ContainsKey(domainServicType)) throw new ApplicationException("不存在對應的Domainservice");
            var servicetype = EventMapping[domainServicType];
            return Bootstrap.Currnet.GetService(servicetype) as IDomainService;
        }

    }
}
