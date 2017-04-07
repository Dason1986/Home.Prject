using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{

    public class StorageEngine : AuditedEntity
    {
        public StorageEngine()
        {
        }

        public StorageEngine(ICreatedInfo createinfo) : base(createinfo)
        {
        }

        [StringLength(50), Required]
        public string Name { get; set; }

        [StringLength(200), Required]
        public string Root { get; set; }

        public Guid SettingID { get; set; }

        public virtual StorageEngineSetting Setting { get; set; }
    }
}