using Library.Domain.DomainEvents;
using DomainModel.ModuleProviders;

namespace DomainModel.DomainServices
{
    public class BuildFingerprintDomainEventHandler : DomainEventHandler<IBuildFingerprintDomainService>
    {
        public BuildFingerprintDomainEventHandler(PhotoItemEventArgs args) : base(args)
        {
        }
    }
    public interface IBuildFingerprintDomainService : IDomainService, IDomainService<PhotoItemEventArgs>
    {
        IGalleryModuleProvider ModuleProvider { get; set; }

        void Handle(Aggregates.GalleryAgg.Photo photo);
        void SetAlgorithm(SimilarAlgorithm type);
    }
}