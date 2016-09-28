using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.GalleryAgg
{
    public class Album : Entity
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Remark { get; set; }

        public DateTime? RecordingDate { get; set; }


        public virtual ICollection<Photo> Photos { get; set; }
    }

    public class Photo : Entity
    {
        public Guid FileID { get; set; }

        public Guid AlbumID { get; set; }
        public virtual Album ParentAlbum { get; set; }
        public virtual FileAgg.FileInfo File { get; set; }
        [StringLength(100)]
        public string Tags { get; set; }

        public PhotoType PhotoType { get; set; }

        public virtual ICollection<PhtotAttribute> Attributes { get; set; }


    }

    public class PhtotAttribute : Entity
    {
        public Guid PhotoID { get; set; }

        public virtual Photo Owner { get; set; }
        [StringLength(50)]
        public string AttKey { get; set; }
        [StringLength(255)]
        public string AttValue { get; set; }
    }
}
