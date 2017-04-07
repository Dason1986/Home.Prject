using Home.DomainModel.Aggregates.FileAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{

    internal class StorageEngineSettingEntityTypeConfiguration : EntityTypeConfiguration<StorageEngineSetting>
    {
        public StorageEngineSettingEntityTypeConfiguration()
        {
            ToTable("StorageEngineSetting");
        }
    }
}