using Library.Domain.DomainEvents;
using DomainModel.ModuleProviders;

namespace DomainModel.DomainServices
{
    public interface IPhotoDomainService :IDomainService, IDomainService<PhotoItemEventArgs>{
        IGalleryModuleProvider ModuleProvider { get; set; }
    }
}