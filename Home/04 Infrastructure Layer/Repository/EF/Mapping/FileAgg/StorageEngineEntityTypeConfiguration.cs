using Home.DomainModel.Aggregates.FileAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{

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
}