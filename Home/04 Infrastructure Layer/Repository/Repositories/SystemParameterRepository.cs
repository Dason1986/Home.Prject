using DomainModel.Aggregates.SystemAgg;
using DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Repository.Repositories
{
	public class SystemParameterRepository : Repository<SystemParameter>, ISystemParameterRepository
	{


		public SystemParameterRepository(EFContext context) : base(context)
		{

		}
	}
}