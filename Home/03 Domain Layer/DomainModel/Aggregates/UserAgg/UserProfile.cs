using Home.DomainModel.Aggregates.ContactAgg;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.UserAgg
{
    public class UserProfile : AuditedEntity
    {
        public UserProfile()
        {
        }

        public Guid ContactProfileID { get; set; }

        public ContactProfile ContactProfile { get; set; }
    }
}

