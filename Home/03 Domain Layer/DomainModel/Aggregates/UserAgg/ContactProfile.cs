using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{

    public class ContactProfile : Entity
    {
        public ContactProfile()
        {
        }


        [StringLength(20)]
        public string Name { get; set; }

        public Gender Six { get; set; }

        public DateTime Birthday { get; set; }





    }

    public enum Gender
    {
        Male,
        Female
    }
}

