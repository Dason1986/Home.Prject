using HomeApplication.Jobs;
using Library;
using System;
using System.Collections.Generic;
using System.Web.Http;
namespace HomeApplication.Owin.API
{
    [RoutePrefix("api/Scander")]
    public class ScanderController : WebAPI
    {
        public IDictionary<Guid, string> Get()
        {
            var job = Bootstrap.Currnet.GetService<ScheduleJobManagement>();
            return job.GetCalendarNames();
        }
        [ActionName("Run")]
        [HttpGet]
        public TimeSpan Run(Guid id)
        {
            var job = Bootstrap.Currnet.GetService<ScheduleJobManagement>();
            return job.RunCalendar(id);
        }
    }
}