using Home.DomainModel.Aggregates.FileAgg;
using Library.Domain.Data;

namespace Home.DomainModel.Repositories
{
    public interface IStorageEngineRepository : IRepository<StorageEngine>
    {
        StorageEngine GetByPathEngine(string path);
    }
}