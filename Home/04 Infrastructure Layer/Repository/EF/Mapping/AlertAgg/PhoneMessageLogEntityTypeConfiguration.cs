using System.Data.Entity.ModelConfiguration;
using Home.DomainModel.Aggregates.AlertAgg;

namespace Home.Repository.EF.Mapping.AlertAgg
{
    internal class PhoneMessageLogEntityTypeConfiguration : EntityTypeConfiguration<PhoneMessageLogEntity>
    {
        public PhoneMessageLogEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Message)
  .WithMany()
  .HasForeignKey(t => t.MessageEntityID)
  .WillCascadeOnDelete(false);
            ToTable("PhoneMessageLogEntity");
        }
    }
}