using DomainModel;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.UserAgg
{
    internal class UserProfileEntityTypeConfiguration : EntityTypeConfiguration<DomainModel.UserAgg.UserProfile>
    {
        public UserProfileEntityTypeConfiguration()
        {
            //     HasRequired(c => c.ContactProfile).WithRequiredPrincipal();
            this.HasRequired(n => n.ContactProfile).WithMany().HasForeignKey(n => n.ContactProfileID);
            ToTable("UserProfile");
        }
    }
    /*
    internal class ContactRelationEntityTypeConfiguration : EntityTypeConfiguration<ContactRelation>
    {
        public ContactRelationEntityTypeConfiguration()
        {
            this.HasRequired(n => n.LeftRole).WithMany().HasForeignKey(n => n.LeftRoleId);
            this.HasRequired(n => n.RightRole).WithMany().HasForeignKey(n => n.RightRoleId);
            ToTable("ContactRelation");
        }
    }
    internal class ContactRelationRightEntityTypeConfiguration : EntityTypeConfiguration<ContactRelationRight>
    {
        public ContactRelationRightEntityTypeConfiguration()
        {

            ToTable("ContactRelationRight");
        }
    }*/
}
