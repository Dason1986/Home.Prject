using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.DomainServices;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library;
using Library.ComponentModel.Logic;
using Library.Domain.Data;
using Library.Infrastructure.Application;
using System;
using System.Linq;
using Quartz;
using Library.HelperUtility;

namespace HomeApplication.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class PhotoFileAnalysis : ScheduleJobProvider
    {


        readonly static object _sync = new object();


        protected string[] ImageTypes;

        protected int Takes;

        public override void Initialize()
        {

        }

        public override void Execute(IJobExecutionContext context)
        {
            lock (_sync)
            {

                provideralleryModule = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();

                var systemParameter = provideralleryModule.CreateSystemParameter();
                var setting = systemParameter.GetListByGroup("PhotoFileAnalysis");
                ImageTypes = setting.Cast<string>("ImageTypes", ".jpeg,.jpg,.png,.bmp").Split(',');
                Takes = setting.Cast<int>("Takes", 10);
                ThreadProssSize(GetTotalRecord());
            }
        }
        IGalleryModuleProvider provideralleryModule;

        protected FileInfo[] GetTotalRecord()
        {

            IFileInfoRepository filesRepository = provideralleryModule.CreateFileInfo();

            var filecount = filesRepository.GetFilesByExtensions(ImageTypes, Takes);
            return filecount;
        }

        void ThreadProssSize(FileInfo[] photolist)
        {
            #region MyRegion



            var domainService = Bootstrap.Currnet.GetService<IAddPhotoDomainService>();
            domainService.ModuleProvider = provideralleryModule;

            IFileInfoRepository filesRepository = domainService.GalleryModuleProvider.CreateFileInfo();
            foreach (var item in photolist)
            {
                domainService.Handle(item);
                domainService.ModuleProvider.UnitOfWork.Commit();
                GC.Collect();
            }



            #endregion MyRegion

        }
    }


}