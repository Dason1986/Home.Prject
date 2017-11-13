using Library.Domain.DomainEvents;

namespace Home.DomainModel.DomainServices
{
    public class BuildFingerprintDomainEventHandler : DomainEventHandler<IBuildFingerprintDomainService>
    {
        public BuildFingerprintDomainEventHandler(PhotoItemEventArgs args)
        {
        }
    }
    public interface IBuildFingerprintDomainService : IPhotoDomainService, IDomainService<PhotoItemEventArgs>
    {


        void Handle(Aggregates.GalleryAgg.Photo photo);
        void SetAlgorithm(SimilarAlgorithm type);
    }
}