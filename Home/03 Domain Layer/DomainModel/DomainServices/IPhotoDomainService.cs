using Library.Domain.DomainEvents;
using Home.DomainModel.ModuleProviders;

namespace Home.DomainModel.DomainServices
{
    public interface IPhotoDomainService : IDomainService, IDomainService<PhotoItemEventArgs>
    {
        IGalleryModuleProvider ModuleProvider { get; set; }
    }
}