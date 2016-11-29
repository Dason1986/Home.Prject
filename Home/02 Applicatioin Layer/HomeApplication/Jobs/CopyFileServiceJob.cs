using Home.DomainModel.JobServices;
using Library;
using Quartz;
using System;

namespace HomeApplication.Jobs
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class CopyFileServiceJob : IJob
    {
        readonly static object _sync = new object();
        public void Execute(IJobExecutionContext context)
        {
            lock (_sync)
            {
                var service = Bootstrap.Currnet.GetService<ICopyFileService>();
                service.DoWork();
            }
        }
    }
    public class CopyFileService : ServiceJob, ICopyFileService
    {


        public CopyFileService()
        {

        }




        public void DoWork()
        {

            try
            {
                //todo:
             



            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Emergency Error");
            }

        }


    }
}
