using Home.DomainModel.Aggregates.SystemAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.SystemAgg
{
    internal class SerialNumberManagementEntityTypeConfiguration : EntityTypeConfiguration<SerialNumberManagement>
    {
        public SerialNumberManagementEntityTypeConfiguration()
        {


            ToTable("SerialNumberManagement");
        }
    }
}