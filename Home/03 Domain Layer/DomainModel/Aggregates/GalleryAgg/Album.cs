using Library.ComponentModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.Aggregates.GalleryAgg
{
    public class Album : Entity
    {
        public Album()
        {

        }
        public Album(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Remark { get; set; }

        public DateTime? RecordingDate { get; set; }


        public virtual ICollection<Photo> Photos { get; set; }
    }
}
