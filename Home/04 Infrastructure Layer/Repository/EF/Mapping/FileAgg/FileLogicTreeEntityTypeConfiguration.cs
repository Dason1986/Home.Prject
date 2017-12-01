using Home.DomainModel.Aggregates.FileAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{
    internal class FileLogicTreeEntityTypeConfiguration : EntityTypeConfiguration<FileLogicTree>
    {
        public FileLogicTreeEntityTypeConfiguration()
        {
            HasRequired(c => c.File)
 .WithMany()
 .HasForeignKey(t => t.FileId)
 .WillCascadeOnDelete(false);

            HasRequired(c => c.Parent)
.WithMany()
.HasForeignKey(t => t.ParentId)
.WillCascadeOnDelete(false);

            HasMany(t => t.Childrens)
.WithRequired()
.HasForeignKey(c => c.ParentId)
.WillCascadeOnDelete(false);
            ToTable("FileLogicTree");
        }
    }
}