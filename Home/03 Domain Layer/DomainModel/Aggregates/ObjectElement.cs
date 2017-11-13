using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates
{

    public abstract class ObjectElement: Entity
    {
        public Guid OwnerID { get; set; }
    }
}