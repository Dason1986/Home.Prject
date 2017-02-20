using Home.DomainModel.Aggregates.ContactAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.UserAgg
{
    public class UserProfile : AuditedEntity
    {
        public UserProfile()
        {
        }
        public UserProfile(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public Guid ContactProfileID { get; set; }

        public virtual  ContactProfile ContactProfile { get; set; }
    }
}

