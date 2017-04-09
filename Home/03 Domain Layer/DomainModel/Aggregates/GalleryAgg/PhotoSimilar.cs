using Library.ComponentModel.Model;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    [System.ComponentModel.Description("相似相處"),
        System.ComponentModel.DisplayName("相似相處")]
    public class PhotoSimilar : CreateEntity
    {
        public PhotoSimilar()
        {
        }

        public PhotoSimilar(ICreatedInfo createinfo) : base(createinfo)
        {
        }
        [System.ComponentModel.Description("右邊相片ID"),
        System.ComponentModel.DisplayName("右邊相片ID")]
        public Guid RightPhotoID { get; set; }

        public virtual Photo RightOwner { get; set; }

        [System.ComponentModel.Description("左邊相片ID"),
        System.ComponentModel.DisplayName("左邊相片ID")]
        public Guid LeftPhotoID { get; set; }

        public virtual Photo LeftOwner { get; set; }
    }
}