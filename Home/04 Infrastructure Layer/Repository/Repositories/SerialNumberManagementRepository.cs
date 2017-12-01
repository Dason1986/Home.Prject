using Home.DomainModel.Aggregates.SystemAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Repository.Repositories
{
    public class SerialNumberManagementRepository : Repository<SerialNumberManagement>, ISerialNumberManagementRepository
    {
        public SerialNumberManagementRepository(DbContext context) : base(context)
        {
        }

    }
}
