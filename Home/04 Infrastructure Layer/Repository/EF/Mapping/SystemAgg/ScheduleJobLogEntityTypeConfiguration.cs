using Home.DomainModel.Aggregates.LogsAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.SystemAgg
{
    internal class ScheduleJobLogEntityTypeConfiguration : EntityTypeConfiguration<ScheduleJobLog>
    {
        public ScheduleJobLogEntityTypeConfiguration()
        {


            ToTable("ScheduleJobLog");
        }
    }
}