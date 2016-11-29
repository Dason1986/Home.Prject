using Library.Domain.DomainEvents;
using System;

namespace Home.DomainModel.DomainServices
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

        public object Tag
        {
            get;

            set;
        }
    }
}