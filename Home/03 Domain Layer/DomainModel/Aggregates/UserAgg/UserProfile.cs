using DomainModel.ContactAgg;
using System;

namespace DomainModel.UserAgg
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

