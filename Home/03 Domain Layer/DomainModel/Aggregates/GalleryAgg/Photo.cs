using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.Aggregates.GalleryAgg
{

    public class Photo : AuditedEntity
    {
        public Photo()
        {

        }
        public Photo(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public Guid FileID { get; set; }

        public Guid AlbumID { get; set; }
        public virtual Album ParentAlbum { get; set; }
        public virtual FileAgg.FileInfo File { get; set; }
        [StringLength(100)]
        public string Tags { get; set; }

        public PhotoType PhotoType { get; set; }

        public virtual ICollection<PhotoAttribute> Attributes { get; set; }


    }

}