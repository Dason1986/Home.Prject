using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{
    [System.ComponentModel.Description("存儲引擎"),
        System.ComponentModel.DisplayName("存儲引擎")]
    public class StorageEngine : AuditedEntity
    {
        public StorageEngine()
        {
        }

        public StorageEngine(ICreatedInfo createinfo) : base(createinfo)
        {
        }

        [StringLength(50), Required]
        [System.ComponentModel.Description("存儲名稱"),
          System.ComponentModel.DisplayName("存儲名稱")]
        public string Name { get; set; }

        [StringLength(200), Required]
        [System.ComponentModel.Description("根"),
          System.ComponentModel.DisplayName("根")]
        public string Root { get; set; }
        [System.ComponentModel.Description("存儲引擎設置編號"),
        System.ComponentModel.DisplayName("存儲引擎設置編號")]
        public Guid SettingID { get; set; }

        public virtual StorageEngineSetting Setting { get; set; }
    }
}