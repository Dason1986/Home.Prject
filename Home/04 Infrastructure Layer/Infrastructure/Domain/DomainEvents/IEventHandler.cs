using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain.DomainEvents
{
    public interface IEventHandler<TEvent>
      where TEvent : IDomainEvent
    {
        void Handle(TEvent @event);
    }
}
