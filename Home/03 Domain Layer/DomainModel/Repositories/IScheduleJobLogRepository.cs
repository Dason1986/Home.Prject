﻿using Home.DomainModel.Aggregates.LogsAgg;
using Library.Domain.Data;
using Library.Domain.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Repositories
{
    public interface IScheduleJobLogRepository : IRepository<ScheduleJobLog>
    {

    }
}
