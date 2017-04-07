using Home.DomainModel.Aggregates.FileAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{

    internal class FileAttributeEntityTypeConfiguration : EntityTypeConfiguration<FileAttribute>
    {
        public FileAttributeEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Owner)
.WithMany()
.HasForeignKey(t => t.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("FileAttribute");
        }
    }
}