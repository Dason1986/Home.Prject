using Home.DomainModel.Aggregates.SystemAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.SystemAgg
{
    internal class ScheduleJobEntityTypeConfiguration : EntityTypeConfiguration<ScheduleJob>
    {
        public ScheduleJobEntityTypeConfiguration()
        {


            ToTable("ScheduleJob");
        }
    }
}