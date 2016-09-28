using Library.ComponentModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Entity : Library.Domain.Entity, IEntity, ICreatedInfo
    {
        public Entity()
        {

        }
        public Entity(ICreatedInfo createinfo)
        {
            ID = Library.IdentityGenerator.NewGuid();
            Created = DateTime.Now;
            CreatedBy = createinfo.CreatedBy;
            StatusCode = StatusCode.Enabled;
        }

        [Required]
        public DateTime Created { get; set; }
        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

    }
}

