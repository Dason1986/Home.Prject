using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF.Mapping.UserAgg
{
    internal class UserProfileEntityTypeConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileEntityTypeConfiguration()
        {
            //     HasRequired(c => c.ContactProfile).WithRequiredPrincipal();
            this.HasRequired(n => n.ContactProfile).WithMany().HasForeignKey(n => n.ContactProfileID);
            ToTable("UserProfile");
        }
    }
    internal class ContactRoleEntityTypeConfiguration : EntityTypeConfiguration<ContactRole>
    {
        public ContactRoleEntityTypeConfiguration()
        {

            ToTable("ContactRole");
        }
    }
    internal class ContactProfileEntityTypeConfiguration : EntityTypeConfiguration<ContactProfile>
    {
        public ContactProfileEntityTypeConfiguration()
        {

            ToTable("ContactProfile");
        }
    }
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
    }
}
