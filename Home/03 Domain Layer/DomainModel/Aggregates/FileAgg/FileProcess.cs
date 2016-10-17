using DomainModel.DomainServices;
using Library.Domain;
using Library.Domain.Data;
using System;
using Library.Domain.DomainEvents;

namespace DomainModel.Aggregates.FileAgg
{

    public class FileProcess : IAggregateRoot
    {
        public FileProcess(Guid fileid)
        {

        }
        DomainEventBus bus = new DomainEventBus();
        public FileInfo File { get; protected set; }

        public Guid ID
        {
            get
            {
                return File.ID;
            }
        }



        public void Commit()
        {

        }

        public void Modify()
        {

        }

        public void Publish()
        {
            bus.Publish();
            //bus.Publish<IAddPhotoDomainService>(new PhotoItemEventArgs());
            //if (CanCreatePhotoInfo())
            //{
            //    CreatePhotoInfo();
            //}
        }
        public bool CanCreatePhotoInfo()
        {
            return false;
        }
        public void CreatePhotoInfo()
        {
            AddEvent(new AddPhotoDomainEventHandler(new PhotoItemEventArgs()));

        }
        public void BuildFingerprint()
        {
            //     AddEvent(new AddPhotoDomainEventHandler(new PhotoItemEventArgs()));

        }
        public void AddEvent(IDomainEventHandler eventHandler)
        {
            bus.AddEvent(eventHandler);
        }
    }
}