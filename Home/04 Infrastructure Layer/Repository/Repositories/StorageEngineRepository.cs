using System.Linq;
using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Data.Entity;

namespace Home.Repository.Repositories
{
    public class StorageEngineRepository : Repository<StorageEngine>, IStorageEngineRepository
    {
        public StorageEngineRepository(DbContext context) : base(context)
        {
        }

        public StorageEngine GetByPathEngine(string path)
        {
            return Wrapper.Find().FirstOrDefault(n => (n.Root.StartsWith(path) || path.StartsWith(n.Root))&& n.StatusCode== Library.ComponentModel.Model.StatusCode.Enabled);
        }
    }
}