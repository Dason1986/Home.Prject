using Library.ComponentModel.Model;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    public class PhotoSimilar : CreateEntity
    {
        public PhotoSimilar()
        {
        }

        public PhotoSimilar(ICreatedInfo createinfo) : base(createinfo)
        {
        }

        public Guid RightPhotoID { get; set; }

        public virtual Photo RightOwner { get; set; }
        public Guid LeftPhotoID { get; set; }

        public virtual Photo LeftOwner { get; set; }
    }
}