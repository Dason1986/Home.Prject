using Home.DomainModel.Aggregates.ContactAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.UserAgg
{
    [System.ComponentModel.Description("操作用戶表"),
          System.ComponentModel.DisplayName("操作用戶表")]
    public class UserProfile : AuditedEntity
    {
        public UserProfile()
        {
        }

        public UserProfile(ICreatedInfo createinfo) : base(createinfo)
        {
        }

        [System.ComponentModel.Description("聯繫人ID"),
       System.ComponentModel.DisplayName("聯繫人ID")]
        public Guid ContactProfileID { get; set; }

        public virtual ContactProfile ContactProfile { get; set; }

        [StringLength(20), Required]
        [System.ComponentModel.Description("員工編號"),
        System.ComponentModel.DisplayName("員工編號")]
        public string StaffNo { get; set; }
    }
}