using Home.DomainModel.Aggregates.FileAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{
    internal class FileInfoEntityTypeConfiguration : EntityTypeConfiguration<FileInfo>
    {
        public FileInfoEntityTypeConfiguration()
        {
            HasRequired(c => c.Photo).WithRequiredPrincipal();

            HasRequired(c => c.Extend).WithRequiredPrincipal();
            ToTable("FileInfo");
        }
    }

    internal class StorageEngineEntityTypeConfiguration : EntityTypeConfiguration<StorageEngine>
    {
        public StorageEngineEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Setting)
 .WithMany()
 .HasForeignKey(t => t.SettingID)
 .WillCascadeOnDelete(false);

            ToTable("StorageEngine");

       
        }
    }

    internal class StorageEngineSettingEntityTypeConfiguration : EntityTypeConfiguration<StorageEngineSetting>
    {
        public StorageEngineSettingEntityTypeConfiguration()
        {
            ToTable("StorageEngineSetting");
        }
    }
}