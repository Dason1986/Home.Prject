using Home.DomainModel.Aggregates.ProductAgg;
using Home.DomainModel.Aggregates.SystemAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.SystemAgg
{
    internal class SystemParameterEntityTypeConfiguration : EntityTypeConfiguration<SystemParameter>
    {
        public SystemParameterEntityTypeConfiguration()
        {


            ToTable("SystemParameter");
        }
    }
}