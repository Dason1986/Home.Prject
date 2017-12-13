using Library.Domain.DomainEvents;

namespace Home.DomainModel.DomainServices
{
    public interface IFileDomainService : IDomainService
    {
        void Handle(DomainModel.Aggregates.FileAgg.FileInfo file);
    }
}