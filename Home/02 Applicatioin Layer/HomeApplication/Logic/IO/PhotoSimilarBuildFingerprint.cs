using DomainModel;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.DomainServices;
using DomainModel.ModuleProviders;
using Library;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using Repository;
using Repository.ModuleProviders;
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
        EmptyOption _option;

        protected override int GetTotalRecord()
        {

            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            var _photoRepository = provider.CreatePhoto();

            var filecount = _photoRepository.GetAll().Count();
            return filecount;

        }

        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);



            var take = BatchSize;

            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            var _photoRepository = provider.CreatePhoto();
            IBuildFingerprintDomainService domainservice = Bootstrap.Currnet.GetService<IBuildFingerprintDomainService>();
            domainservice.ModuleProvider = provider;
            //   domainservice.SetAlgorithm( SimilarAlgorithm.);
            IList<Photo> photolist = _photoRepository.GetList(beginindex,take);

            foreach (var item in photolist)
            {
                domainservice.Handle(item);
            }

        }
    }
}