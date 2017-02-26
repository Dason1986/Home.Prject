using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.ContactAgg
{
    public class FamilyRelation : CreateEntity
    {
        public FamilyRelation()
        {

        }
        public FamilyRelation(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public Guid LeftRoleId { get; set; }
        public Guid RightRoleId { get; set; }
        public virtual FamilyRole LeftRole { get; set; }
        public virtual FamilyRole RightRole { get; set; }

    

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

