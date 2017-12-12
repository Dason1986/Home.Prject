using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    [System.ComponentModel.Description("相片"),
        System.ComponentModel.DisplayName("相片")]
    public class Photo : AuditedEntity
    {
        public Photo()
        {

        }
        public Photo(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("文件ID"),
       System.ComponentModel.DisplayName("文件ID")]
        public Guid FileID { get; set; }
        public virtual DomainModel.Aggregates.FileAgg.FileInfo File { get; set; }
        [System.ComponentModel.Description("相冊ID"),
        System.ComponentModel.DisplayName("相冊ID")]
        public Guid AlbumID { get; set; }
        public virtual Album ParentAlbum { get; set; }

        [StringLength(100)]
        [System.ComponentModel.Description("分類標籤"),
          System.ComponentModel.DisplayName("分類標籤")]
        public string Tags { get; set; }


        [System.ComponentModel.Description("相片屬性"),
        System.ComponentModel.DisplayName("相片屬性")]
        public virtual ICollection<PhotoAttribute> Attributes { get; set; }
        [System.ComponentModel.Description("頭像"),
        System.ComponentModel.DisplayName("頭像")]
        public virtual ICollection<PhotoFace> Faces { get; set; }


    }

}