using Library.ComponentModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Entity : IEntity, ICreatedInfo
    {
        [Required]
        [Key]
        public Guid ID { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}

