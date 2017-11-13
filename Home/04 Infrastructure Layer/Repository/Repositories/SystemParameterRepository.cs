using System.Linq;
using Home.DomainModel.Aggregates.SystemAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Data.Entity;

namespace Home.Repository.Repositories
{
    public class SystemParameterRepository : Repository<SystemParameter>, ISystemParameterRepository
    {
        public SystemParameterRepository(DbContext context) : base(context)
        {
        }

        public SystemParameter[] GetListByGroup(string groupname)
        {
            return GetAll().Where(n => n.ParameterGroup == groupname).ToArray();
        }
    }
}