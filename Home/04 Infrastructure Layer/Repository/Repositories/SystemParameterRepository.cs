using Home.DomainModel.Aggregates.SystemAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Home.Repository.Repositories
{
	public class SystemParameterRepository : Repository<SystemParameter>, ISystemParameterRepository
	{


		public SystemParameterRepository(EFContext context) : base(context)
		{

		}
	}
}