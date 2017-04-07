using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{

    public class StorageEngineSetting : AuditedEntity
    {
        [StringLength(200)]
        public string Host { get; set; }

        [StringLength(200)]
        public string Uid { get; set; }

        [StringLength(200)]
        public string Pwd { get; set; }
    }
}