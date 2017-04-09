using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.ContactAgg
{
    [System.ComponentModel.Description("联系人"),
        System.ComponentModel.DisplayName("联系人")]
    public class ContactProfile : AuditedEntity
    {
        public ContactProfile()
        {
        }
        public ContactProfile(ICreatedInfo createinfo) : base(createinfo)
        {

        }

        [StringLength(20)]
        [System.ComponentModel.Description("名称"),
          System.ComponentModel.DisplayName("名称")]
        public string Name { get; set; }
        [System.ComponentModel.Description("性别"),
        System.ComponentModel.DisplayName("性别")]
        public Gender Six { get; set; }
        [System.ComponentModel.Description("生日"),
        System.ComponentModel.DisplayName("生日")]
        public DateTime Birthday { get; set; }





    }
}

