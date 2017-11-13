using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Data.Entity;
using Home.DomainModel.Aggregates.SystemAgg;

namespace Home.Repository.Repositories
{
    public class ScheduleJobRepository : Repository<ScheduleJob>, IScheduleJobRepository
    {
        public ScheduleJobRepository(DbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
