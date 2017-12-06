using Home.DomainModel.ModuleProviders;
using Library;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using Home.Repository;
using Home.Repository.ModuleProviders;
using System.Collections.Generic;
using System.Linq;
using HomeApplication.DomainServices;
using System.IO;
using Quartz;

namespace HomeApplication.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class DeleteFileDistinctByMD5 : ScheduleJobProvider
    {
        readonly static object _sync = new object();
        public override void Execute(IJobExecutionContext context)
        {
            lock (_sync)
            {

                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();

                var fileInfoRepository = provider.CreateFileInfo();
                var photoRepository = provider.CreatePhoto();
                var md5S = fileInfoRepository.GetFileDuplicateByMD5();
                if (md5S.Length == 0) return;
            }
        }

        public override void Initialize()
        {

        }
    }
}