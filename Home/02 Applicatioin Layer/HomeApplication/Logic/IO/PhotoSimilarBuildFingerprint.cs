using Home.DomainModel;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.DomainServices;
using Home.DomainModel.ModuleProviders;
using Library;
using Library.ComponentModel.Logic;
using Library.Domain.Data;
using Library.Infrastructure.Application;
using Home.Repository;
using Home.Repository.ModuleProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HomeApplication.Logic.IO
{
    public class PhotoSimilarBuildFingerprint : BaseMultiThreadingLogicService
    {
        protected override IOption ServiceOption
        {
            get
            {
                return _option;
            }

            set
            {
                _option = (EmptyOption)value;
            }
        }

        private EmptyOption _option;

        protected override int GetTotalRecord()
        {
            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            var _photoRepository = provider.CreatePhoto();

            var filecount = _photoRepository.GetAll().Count();
            provider.Dispose();
            return filecount;
        }

        protected override void ThreadProssSize(int beginindex, int endindex, int take)
        {
            using (var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>())
            {
                IBuildFingerprintDomainService domainservice = Bootstrap.Currnet.GetService<IBuildFingerprintDomainService>();
                domainservice.ModuleProvider = (provider);
                //   domainservice.SetAlgorithm( SimilarAlgorithm.);
                var _photoRepository = domainservice.ModuleProvider.CreatePhoto();
                IList<Photo> photolist = _photoRepository.GetList(beginindex, take);

                foreach (var item in photolist)
                {
                    domainservice.Handle(item);
                    domainservice.ModuleProvider.UnitOfWork.Commit();
                }
            }
        }
    }
}