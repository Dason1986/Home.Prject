using Home.DomainModel.Aggregates.SystemAgg;
using Library.Domain.Data;

namespace Home.DomainModel.Repositories
{
    public interface ISystemParameterRepository : IRepository<SystemParameter>
    {
        SystemParameter[] GetListByGroup(string groupname);
    }
}