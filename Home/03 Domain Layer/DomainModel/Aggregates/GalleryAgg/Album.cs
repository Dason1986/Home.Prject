using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    [System.ComponentModel.Description("相冊"),
           System.ComponentModel.DisplayName("相冊")]
    public class Album : AuditedEntity
    {
        public Album()
        {

        }
        public Album(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(50)]
        [System.ComponentModel.Description("名稱"),
          System.ComponentModel.DisplayName("名稱")]
        public string Name { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("備註"),
          System.ComponentModel.DisplayName("備註")]
        public string Remark { get; set; }
        [System.ComponentModel.Description("日期"),
        System.ComponentModel.DisplayName("日期")]
        public DateTime? RecordingDate { get; set; }

        [System.ComponentModel.Description("相片集合"),
        System.ComponentModel.DisplayName("相片集合")]
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
