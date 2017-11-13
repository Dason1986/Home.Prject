using Home.DomainModel.Aggregates.SystemAgg;
using Library.ComponentModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.LogsAgg
{
    public class ScheduleJobLog : Library.Domain.Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public ScheduleJobLog() { }
        public ScheduleJobLog(ICreatedInfo createinfo) : base(createinfo) { }
        [Description(@"排程編號")]
        public Guid ScheduleId { get; set; }
        public virtual ScheduleJob Schedule { get; set; }
        [Description(@"執行用時")]
        public TimeSpan ElapsedTime { get; set; }
        [Description(@"執行是否出錯")]
        public bool HasError { get; set; }
    }
}
