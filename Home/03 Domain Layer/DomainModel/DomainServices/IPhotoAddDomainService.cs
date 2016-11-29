using Library.Domain.DomainEvents;

namespace Home.DomainModel.DomainServices
{


    public class AddPhotoDomainEventHandler : DomainEventHandler<IAddPhotoDomainService>
    {
        public AddPhotoDomainEventHandler(PhotoItemEventArgs args) : base(args)
        {
        }
    }
    public interface IAddPhotoDomainService : IPhotoDomainService 
    {
       

        void Handle(Aggregates.GalleryAgg.Photo photo, DomainModel.Aggregates.FileAgg.FileInfo file);
    }
}
