using HomeApplication.Jobs;
using Library;
using System;
using System.Collections.Generic;
using System.Web.Http;
namespace HomeApplication.Owin.API
{
 
    public class ScanderController : WebAPI
    {
        [Route("api/Scander")]
        public IDictionary<Guid, string> Get()
        {
            var job = Bootstrap.Currnet.GetService<ScheduleJobManagement>();
            return job.GetCalendarNames();
        }
        [Route("api/Scander/run/{id}")]     
        public TimeSpan Run(Guid id)
        {
            var job = Bootstrap.Currnet.GetService<ScheduleJobManagement>();
            return job.RunCalendar(id);
        }
    }
}