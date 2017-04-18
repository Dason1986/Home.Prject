using System.Data.Entity.ModelConfiguration;
using Home.DomainModel.Aggregates.AlertAgg;

namespace Home.Repository.EF.Mapping.AlertAgg
{
    internal class MailMessageLogEntityTypeConfiguration : EntityTypeConfiguration<MailMessageLogEntity>
    {
        public MailMessageLogEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Message)
.WithMany()
.HasForeignKey(t => t.MessageEntityID)
.WillCascadeOnDelete(false);
            ToTable("MailMessageLogEntity");
        }
    }
}