using Library.Domain.DomainEvents;
using System;
using DomainModel.ModuleProviders;

namespace DomainModel.DomainServices
{
    public class PhotoItemEventArgs : IDomainEventArgs
    {
        public PhotoItemEventArgs(Guid fileID, Guid photoID)
        {
            FileID = fileID;
            PhotoID = photoID;
        }
        public PhotoItemEventArgs()
        {

        }

        public Guid FileID { get;protected set; }

        public Guid PhotoID { get; protected set; }
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
