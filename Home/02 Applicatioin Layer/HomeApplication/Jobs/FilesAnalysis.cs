using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.DomainServices;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class FilesAnalysis : ScheduleJobProvider
    {
        readonly static object _sync = new object();
        // 
      
        public override void Execute(IJobExecutionContext context)
        {
            lock (_sync)
            {
                IFileManagentModuleProvider FileModuleProvider = Library.Bootstrap.Currnet.GetService<IFileManagentModuleProvider>();
              
                IFileInfoRepository filesRepository = FileModuleProvider.CreateFileInfo();

                var filecount = filesRepository.GetFilesByExtensions(null, 10);
                foreach (var item in filecount)
                {
                    
                   
                        GC.Collect();
                   
                  
                }
            }
        }
      

        public override void Initialize()
        {

        }
    }
}
