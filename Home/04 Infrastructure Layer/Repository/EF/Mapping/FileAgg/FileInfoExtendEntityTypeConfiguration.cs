using Home.DomainModel.Aggregates.FileAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{

    internal class FileInfoExtendEntityTypeConfiguration : EntityTypeConfiguration<FileInfoExtend>
    {
        public FileInfoExtendEntityTypeConfiguration()
        {
            ToTable("FileInfoExtend");
        }
    }
}