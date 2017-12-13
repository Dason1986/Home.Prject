using Library.Domain.DomainEvents;

namespace Home.DomainModel.DomainServices
{
    public class AddPhotoDomainEventHandler : DomainEventHandler<IAddPhotoDomainService>
    {
        public AddPhotoDomainEventHandler(PhotoItemEventArgs args)
        {
        }
    }

    public interface IAddPhotoDomainService : IPhotoDomainService
    {
   

      
    }
}