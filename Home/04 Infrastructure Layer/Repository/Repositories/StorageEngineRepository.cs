using System.Linq;
using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Home.Repository.Repositories
{
    public class StorageEngineRepository : Repository<StorageEngine>, IStorageEngineRepository
    {
        public StorageEngineRepository(EFContext context) : base(context)
        {
        }

        public StorageEngine GetByPathEngine(string path)
        {
            return GetAll().FirstOrDefault(n => n.Root.StartsWith(path) || path.StartsWith(n.Root));
        }
    }
}