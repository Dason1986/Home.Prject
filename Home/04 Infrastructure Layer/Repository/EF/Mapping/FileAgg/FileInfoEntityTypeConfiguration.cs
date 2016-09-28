using DomainModel;
using DomainModel.Aggregates.FileAgg;
using DomainModel.ContactAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{
    internal class FileInfoEntityTypeConfiguration : EntityTypeConfiguration<FileInfo>
    {
        public FileInfoEntityTypeConfiguration()
        {

            ToTable("FileInfo");
        }
    }
}