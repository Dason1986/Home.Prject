using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates
{

    public abstract class ObjectElement: CreateEntity
    {
        public Guid OwnerID { get; set; }
    }
}