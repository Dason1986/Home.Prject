using Library.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.ModuleProviders;

namespace DomainModel.DomainServices
{
    public class PhotoItemEventArgs : IDomainEventArgs
    {


        public Guid FileID { get; set; }

        public Guid PhotoID { get; set; }
    }


    public class AddPhotoDomainEventHandler : DomainEventHandler<IAddPhotoDomainService>
    {
        public AddPhotoDomainEventHandler(PhotoItemEventArgs args) : base(args)
        {
        }

       


    }

    public interface IAddPhotoDomainService : IDomainService, IDomainService<PhotoItemEventArgs>
    {
        IGalleryModuleProvider ModuleProvider { get; set; }

        void Handle(Aggregates.GalleryAgg.Photo photo, DomainModel.Aggregates.FileAgg.FileInfo file);
    }
}
