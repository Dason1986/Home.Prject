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
    public class OfficeFilesAnalysis : ScheduleJobProvider
    {
        readonly static object _sync = new object();
        // 
        readonly static string[] Extensions = { ".doc", ".docx", ".dot", ".dotx", ".xlsx", ".xltx", ".xls", ".pptx", ".potx", ".pdf" };
        public override void Execute(IJobExecutionContext context)
        {
            lock (_sync)
            {
                IFileManagentModuleProvider FileModuleProvider = Library.Bootstrap.Currnet.GetService<IFileManagentModuleProvider>();
                IOfficeFileDomainService domainService = Library.Bootstrap.Currnet.GetService<IOfficeFileDomainService>();
                domainService.FileModuleProvider = FileModuleProvider;
                IFileInfoRepository filesRepository = FileModuleProvider.CreateFileInfo();

                var filecount = filesRepository.GetFilesByExtensions(Extensions, 10);
                foreach (var item in filecount)
                {
                    
                        domainService.Handle(item);
                        domainService.ModuleProvider.UnitOfWork.Commit();
                        GC.Collect();
                   
                  
                }
            }
        }
      

        public override void Initialize()
        {

        }
    }
}
