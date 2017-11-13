using Home.DomainModel.Aggregates.FileAgg;
using Library.Domain.Data;
using Library.Domain.Data.Repositorys;

namespace Home.DomainModel.Repositories
{
    public interface IStorageEngineRepository : IRepository<StorageEngine>
    {
        StorageEngine GetByPathEngine(string path);
    }
}