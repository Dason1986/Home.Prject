using Library.ComponentModel.Model;
using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.ContactAgg
{
    [System.ComponentModel.Description("家庭角色"),
        System.ComponentModel.DisplayName("家庭角色")]
    public class FamilyRole : Entity
    {
        public FamilyRole()
        {

        }
        public FamilyRole(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(20)]
        [System.ComponentModel.Description("角色名稱"),
          System.ComponentModel.DisplayName("角色名稱")]
        public string Name { get; set; }

        [StringLength(20)]
        public string Remark { get; set; }

        [System.ComponentModel.Description("等級，以自己爲基準，比自己大一輩的+1，比自己小一輩的-1"),
        System.ComponentModel.DisplayName("等級")]
        public int Level { get; set; }
        [System.ComponentModel.Description("性別"),
        System.ComponentModel.DisplayName("性別")]
        public Gender Six { get; set; }
        public void AddMemberAddress(MemberAddress[] members) {

        }
    }
}