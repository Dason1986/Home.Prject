using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.GalleryAgg
{

    public class PhotoAttribute : AuditedEntity
    {
        public PhotoAttribute()
        {

        }
        public PhotoAttribute(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public Guid PhotoID { get; set; }

        public virtual Photo Owner { get; set; }
        [StringLength(50)]
        public string AttKey { get; set; }
        [StringLength(255)]
        public string AttValue { get; set; }
        public byte[] BitValue { get; set; }
    }
}