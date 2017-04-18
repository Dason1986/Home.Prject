using System.Data.Entity.ModelConfiguration;
using Home.DomainModel.Aggregates.AlertAgg;

namespace Home.Repository.EF.Mapping.AlertAgg
{
    internal class MessageEntityEntityTypeConfiguration : EntityTypeConfiguration<MessageEntity>
    {
        public MessageEntityEntityTypeConfiguration()
        {
            HasMany(c => c.Logs)
.WithRequired()
.HasForeignKey(c => c.MessageEntityID)
.WillCascadeOnDelete(false);
            ToTable("MessageEntity");
        }
    }
}