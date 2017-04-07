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
        public Guid OwnerID { get; set; }
        [StringLength(50)]
        public string AttKey { get; set; }
        [StringLength(255)]
        public string AttValue { get; set; }
    }
}