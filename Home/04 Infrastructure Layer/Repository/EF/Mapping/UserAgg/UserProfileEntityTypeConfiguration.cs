 
using Home.DomainModel.Aggregates.UserAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.UserAgg
{
    internal class UserProfileEntityTypeConfiguration : EntityTypeConfiguration< UserProfile>
    {
        public UserProfileEntityTypeConfiguration()
        { 
            this.HasRequired(n => n.ContactProfile).WithMany().HasForeignKey(n => n.ContactProfileID);
            ToTable("UserProfile");
        }
    }
    
}
