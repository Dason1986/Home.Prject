using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates
{
    public abstract class Attribute : AuditedEntity
    {
        public Attribute()
        {

        }
        public Attribute(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("所屬上級編號"),
        System.ComponentModel.DisplayName("所屬上級編號")]
        public Guid OwnerID { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("主鍵"),
          System.ComponentModel.DisplayName("主鍵")]
        public string AttKey { get; set; }
        [StringLength(255)]
        [System.ComponentModel.Description("內容"),
          System.ComponentModel.DisplayName("內容")]
        public string AttValue { get; set; }
    }
}