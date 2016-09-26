using System;

namespace DomainModel
{
    public class UserProfile : Entity
    {
        public UserProfile()
        {
        }

        public Guid ContactProfileID { get; set; }

        public ContactProfile ContactProfile { get; set; }
    }
}

