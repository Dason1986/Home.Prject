using Library.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.DomainServices
{
    public class PhotoFacesDomainEventHandler : DomainEventHandler<IPhotoFacesDomainService>
    {
        public PhotoFacesDomainEventHandler(PhotoItemEventArgs args) 
        {
        }
    }
    public interface IPhotoFacesDomainService : IDomainService
    {
    }
}
