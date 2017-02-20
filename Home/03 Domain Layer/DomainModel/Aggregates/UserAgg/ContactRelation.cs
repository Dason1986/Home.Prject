using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class ContactRelation : CreateEntity
    {
        public ContactRelation()
        {

        }
        public ContactRelation(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

    }

    
}

