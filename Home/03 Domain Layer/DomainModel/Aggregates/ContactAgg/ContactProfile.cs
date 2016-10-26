using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.Aggregates.ContactAgg
{

    public class ContactProfile : AuditedEntity
    {
        public ContactProfile()
        {
        }


        [StringLength(20)]
        public string Name { get; set; }

        public Gender Six { get; set; }

        public DateTime Birthday { get; set; }





    }
}

