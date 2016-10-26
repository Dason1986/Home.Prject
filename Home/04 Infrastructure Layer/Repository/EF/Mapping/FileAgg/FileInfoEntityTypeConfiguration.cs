using DomainModel.Aggregates.FileAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.FileAgg
{
    internal class FileInfoEntityTypeConfiguration : EntityTypeConfiguration<FileInfo>
    {
        public FileInfoEntityTypeConfiguration()
        {

            HasRequired(c => c.Photo).WithRequiredPrincipal();
            ToTable("FileInfo");
        }
    }
}