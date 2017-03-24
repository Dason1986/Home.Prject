using Home.DomainModel.Aggregates.ContactAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

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

        public virtual ContactProfile ContactProfile { get; set; }

        [StringLength(20), Required]
        public string StaffNo { get; set; }
    }
}