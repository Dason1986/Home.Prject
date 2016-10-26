using Library.Domain.DomainEvents;

namespace DomainModel.DomainServices
{
    public class BuildFingerprintDomainEventHandler : DomainEventHandler<IBuildFingerprintDomainService>
    {
        public BuildFingerprintDomainEventHandler(PhotoItemEventArgs args) : base(args)
        {
        }
    }
    public interface IBuildFingerprintDomainService : IPhotoDomainService, IDomainService<PhotoItemEventArgs>
    {


        void Handle(Aggregates.GalleryAgg.Photo photo);
        void SetAlgorithm(SimilarAlgorithm type);
    }
}