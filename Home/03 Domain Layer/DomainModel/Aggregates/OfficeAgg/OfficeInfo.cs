using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.OfficeAgg
{
    public abstract class OfficeInfo : AuditedEntity
    {
        [StringLength(50)]
        [System.ComponentModel.Description("作者"),
          System.ComponentModel.DisplayName("作者")]
        public string Author { get; set; }

        [StringLength(50)]
        [System.ComponentModel.Description("主題"),
          System.ComponentModel.DisplayName("主題")]
        public string Suject { get; set; }

        [StringLength(50)]
        [System.ComponentModel.Description("標題"),
          System.ComponentModel.DisplayName("標題")]
        public string Title { get; set; }

        [StringLength(255)]
        [System.ComponentModel.Description("關鍵字"),
          System.ComponentModel.DisplayName("關鍵字")]
        public string KeyWorks { get; set; }
    }
}