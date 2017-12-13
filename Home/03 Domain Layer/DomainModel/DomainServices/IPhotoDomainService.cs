using Library.Domain.DomainEvents;
using Home.DomainModel.ModuleProviders;

namespace Home.DomainModel.DomainServices
{
    public interface IPhotoDomainService : IFileDomainService, IDomainService<PhotoItemEventArgs>
    {
        IGalleryModuleProvider GalleryModuleProvider { get; set; }
    }
}