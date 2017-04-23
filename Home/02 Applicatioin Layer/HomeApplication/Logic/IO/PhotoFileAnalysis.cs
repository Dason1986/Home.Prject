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

namespace HomeApplication.Logic.IO
{
    public class PhotoFileAnalysis : BaseMultiThreadingLogicService
    {
        public PhotoFileAnalysisOption Option
        {
            get { return _option; }
            set
            {
                _option = value;
            }
        }

        private PhotoFileAnalysisOption _option;

        protected override IOption ServiceOption
        {
            get
            {
                return Option;
            }

            set
            {
                Option = (PhotoFileAnalysisOption)value;
            }
        }

        private PhotoFileAnalysisProvider _photoFileAnalysisProvider;

        protected override bool OnVerification()
        {
            _photoFileAnalysisProvider = new PhotoFileAnalysisByDb(this);

            return base.OnVerification();
        }

        protected override int GetTotalRecord()
        {
            return _photoFileAnalysisProvider.GetTotalRecord();
        }

        protected override void ThreadProssSize(int beginindex, int endindex, int take)
        {
            _photoFileAnalysisProvider.ThreadProssSize(beginindex, endindex, take);
        }

        private abstract class PhotoFileAnalysisProvider
        {
            protected PhotoFileAnalysis Analysis { get; private set; }

            protected PhotoFileAnalysisProvider(PhotoFileAnalysis analysis)
            {
                Analysis = analysis;
            }

            public abstract int GetTotalRecord();

            public abstract void ThreadProssSize(int beginindex, int endindex, int take);
        }

        private class PhotoFileAnalysisByDb : PhotoFileAnalysisProvider
        {
            public PhotoFileAnalysisByDb(PhotoFileAnalysis analysis) : base(analysis)
            {
            }

            public override int GetTotalRecord()
            {
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository filesRepository = provider.CreateFileInfo();

                var filecount = filesRepository.GetPhotoFilesByExtensions(Analysis.Option.ImageTypes).Count();
                return filecount;
            }

            public override void ThreadProssSize(int beginindex, int endindex, int take)
            {
                #region MyRegion

                using (var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>())
                {
                    var domainService = Bootstrap.Currnet.GetService<IAddPhotoDomainService>();
                    domainService.ModuleProvider = provider;

                    IFileInfoRepository filesRepository = domainService.ModuleProvider.CreateFileInfo();
                    var photolist = filesRepository.GetPhotoFilesByExtensions(Analysis.Option.ImageTypes).Skip(beginindex).Take(take).ToList();
                    foreach (var item in photolist)
                    {
                        domainService.Handle( item);
                        domainService.ModuleProvider.UnitOfWork.Commit();
                        GC.Collect();
                    }
               
                }

                #endregion MyRegion
            }
        }
    }

    public struct PhotoFileAnalysisOption : IOption
    {
        public string[] ImageTypes { get; set; }
    }
}