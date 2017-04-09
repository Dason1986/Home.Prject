using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{
    [System.ComponentModel.Description("存儲器設置"),
        System.ComponentModel.DisplayName("存儲器設置")]
    public class StorageEngineSetting : AuditedEntity
    {
        [StringLength(200)]
        [System.ComponentModel.Description("主機"),
          System.ComponentModel.DisplayName("主機")]
        public string Host { get; set; }

        [StringLength(200)]
        [System.ComponentModel.Description("用戶"),
          System.ComponentModel.DisplayName("用戶")]
        public string Uid { get; set; }

        [StringLength(200)]
        [System.ComponentModel.Description("密碼"),
          System.ComponentModel.DisplayName("密碼")]
        public string Pwd { get; set; }
    }
}