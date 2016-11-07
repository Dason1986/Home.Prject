using Library.Domain.DomainEvents;
using DomainModel.ModuleProviders;

namespace DomainModel.DomainServices
{
    public interface IPhotoDomainService : IDomainService, IDomainService<PhotoItemEventArgs>
    {
        new IGalleryModuleProvider ModuleProvider { get; set; }
    }
}