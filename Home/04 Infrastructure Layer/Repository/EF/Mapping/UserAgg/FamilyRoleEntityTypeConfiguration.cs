
using Home.DomainModel.Aggregates.ContactAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.UserAgg
{
    internal class FamilyRoleEntityTypeConfiguration : EntityTypeConfiguration<FamilyRole>
    {
        public FamilyRoleEntityTypeConfiguration()
        {

            ToTable("FamilyRole");
        }
    }
    internal class ContactRelationEntityTypeConfiguration : EntityTypeConfiguration<FamilyRelation>
    {
        public ContactRelationEntityTypeConfiguration()
        {
          

            this.HasRequired(t => t.RightRole)
  .WithMany()
  .HasForeignKey(t => t.RightRoleId)
  .WillCascadeOnDelete(false);

            this.HasRequired(t => t.LeftRole)
  .WithMany()
  .HasForeignKey(t => t.LeftRoleId)
  .WillCascadeOnDelete(false);
            ToTable("FamilyRelation");
        }
    }
}