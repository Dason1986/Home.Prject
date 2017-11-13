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
    public class PhotoFileAnalysis : ScheduleJobProvider
    {





        protected string[] ImageTypes;

        protected int Takes;

        public override void Initialize()
        {

        }

        public override void Execute(IJobExecutionContext context)
        {

            var provider = Bootstrap.Currnet.GetService<IFileManagentModuleProvider>();
            var systemParameter = provider.CreateSystemParameter();
            var setting = systemParameter.GetListByGroup("PhotoFileAnalysis");
            ImageTypes = setting.Cast<string>("ImageTypes", "").Split(',');
            Takes = setting.Cast<int>("Takes", 10);
            ThreadProssSize(GetTotalRecord());
        }


        protected FileInfo[] GetTotalRecord()
        {
            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            IFileInfoRepository filesRepository = provider.CreateFileInfo();

            var filecount = filesRepository.GetPhotoFilesByExtensions(ImageTypes,Takes);
            return filecount;
        }

        void ThreadProssSize(FileInfo[] photolist)
        {
            #region MyRegion

            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            {
                var domainService = Bootstrap.Currnet.GetService<IAddPhotoDomainService>();
                domainService.ModuleProvider = provider;

                IFileInfoRepository filesRepository = domainService.GalleryModuleProvider.CreateFileInfo();
                foreach (var item in photolist)
                {
                    domainService.Handle(item);
                    domainService.ModuleProvider.UnitOfWork.Commit();
                    GC.Collect();
                }

            }

            #endregion MyRegion

        }
    }


}