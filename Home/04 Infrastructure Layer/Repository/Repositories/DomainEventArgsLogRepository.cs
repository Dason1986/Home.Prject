using Home.DomainModel.Aggregates.LogsAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Repository.Repositories
{
    public class DomainEventArgsLogRepository : Repository<DomainEventArgsLog>, IDomainEventArgsLogRepository
    {
        public DomainEventArgsLogRepository(System.Data.Entity.DbContext dbcontext) : base(dbcontext)
        {


        }
        public DomainEventArgsLog[] GetAllEvents()
        {
            return Wrapper.Find().Where(n => !n.IsExecuted).Take(5).ToArray();
        }
    }
}
