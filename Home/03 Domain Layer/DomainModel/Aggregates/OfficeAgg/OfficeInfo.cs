using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.OfficeAgg
{
    public abstract class OfficeInfo : AuditedEntity
    {
        [StringLength(50)]
        public string Author { get; set; }

        [StringLength(50)]
        public string Suject { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(255)]
        public string KeyWorks { get; set; }
    }
}