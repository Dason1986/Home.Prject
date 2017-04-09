using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.ContactAgg
{
    [System.ComponentModel.Description("家庭關係"),
        System.ComponentModel.DisplayName("家庭關係")]
    public class FamilyRelation : CreateEntity
    {
        public FamilyRelation()
        {

        }
        public FamilyRelation(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("左邊角色ID"),
       System.ComponentModel.DisplayName("左邊角色ID")]
        public Guid LeftRoleId { get; set; }
        [System.ComponentModel.Description("右邊角色ID"),
System.ComponentModel.DisplayName("右邊角色ID")]
        public Guid RightRoleId { get; set; }
        public virtual FamilyRole LeftRole { get; set; }
        public virtual FamilyRole RightRole { get; set; }


        [System.ComponentModel.Description("備註"),
        System.ComponentModel.DisplayName("備註")]
        [StringLength(100)]
        public string Remark { get; set; }


        public void AddMember()
        {

        }

        public static FamilyRelation Create(ICreatedInfo created, FamilyRole left, FamilyRole right)
        {
            if (created == null) throw new ArgumentNullException();
            if (left == null) throw new ArgumentNullException();
            if (right == null) throw new ArgumentNullException();
            if (left.Six !=  Gender.Male) throw new ArgumentNullException();
            if (right.Six != Gender.Female) throw new ArgumentNullException();
            var relation = new FamilyRelation(created)
            {
                LeftRoleId=left.ID,
                RightRoleId=right.ID,
                LeftRole=left,
                RightRole=right
            };

            return relation;
        }
    }
  

}

