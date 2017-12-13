using System;

namespace Home.DomainModel.Aggregates
{
    public interface IAggregate<TEntity> where TEntity : class, Library.ComponentModel.Model.IAggregateRoot<Guid>
    {
    
        TEntity Entity { get; }
        void Commit();
    }
}