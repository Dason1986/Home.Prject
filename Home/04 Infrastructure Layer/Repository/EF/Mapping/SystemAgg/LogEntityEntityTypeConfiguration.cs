using Home.DomainModel.Aggregates.LogsAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.SystemAgg
{
    internal class LogEntityEntityTypeConfiguration : EntityTypeConfiguration<LogEntity>
    {
        public LogEntityEntityTypeConfiguration()
        {


            ToTable("LogEntity");
        }
    }
}