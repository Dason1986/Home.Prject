using Home.DomainModel.Aggregates.LogsAgg;
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
    internal class DomainEventArgsLogEntityTypeConfiguration : EntityTypeConfiguration<DomainEventArgsLog>
    {
        public DomainEventArgsLogEntityTypeConfiguration()
        {


            ToTable("DomainEventArgsLog");
        }
    }
    internal class ScheduleJobEntityTypeConfiguration : EntityTypeConfiguration<ScheduleJob>
    {
        public ScheduleJobEntityTypeConfiguration()
        {


            ToTable("ScheduleJob");
        }
    }
    internal class ScheduleJobLogEntityTypeConfiguration : EntityTypeConfiguration<ScheduleJobLog>
    {
        public ScheduleJobLogEntityTypeConfiguration()
        {


            ToTable("ScheduleJobLog");
        }
    }
}