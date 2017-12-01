using Home.DomainModel.Aggregates.LogsAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.SystemAgg
{
    internal class DomainEventArgsLogEntityTypeConfiguration : EntityTypeConfiguration<DomainEventArgsLog>
    {
        public DomainEventArgsLogEntityTypeConfiguration()
        {


            ToTable("DomainEventArgsLog");
        }
    }
}