using Home.DomainModel.Aggregates.LogsAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Home.Repository.Repositories
{
    public class ScheduleJobLogRepository : Repository<ScheduleJobLog>, IScheduleJobLogRepository
    {
        public ScheduleJobLogRepository(DbContext dbcontext) : base(dbcontext)
        {
        }
    } }
