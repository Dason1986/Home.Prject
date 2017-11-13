using System.Linq;
using Home.DomainModel.Aggregates.GalleryAgg;
using System.Collections.Generic;
using Home.DomainModel.ModuleProviders;
using Library;
using Home.DomainModel.Repositories;
using Library.ComponentModel.Logic;
using Home.DomainModel.DomainServices;
using Library.Infrastructure.Application;
using Library.Comparable;

namespace HomeApplication.Logic.IO
{
    public class PhotoSimilarOption : IOption
    {
        public double Similarity { get; set; }

        public Home.DomainModel.SimilarAlgorithm AlgorithmType { get; set; }
    }
    public class PhotoSimilar : BaseLogicService
    {
        public PhotoSimilar()
        {
            BatchSize = 500;


        }


        public PhotoSimilarOption Option
        {
            get { return _option; }
            set
            {
                _option = value;

            }
        }

        PhotoSimilarOption _option;
        protected override IOption ServiceOption
        {
            get
            {
                return Option;
            }

            set
            {
                Option = (PhotoSimilarOption)value;
            }
        }

        public int BatchSize { get; private set; }

        protected virtual int GetTotalRecord()
        {

            //   using (MainBoundedContext dbcontext = new MainBoundedContext())
            {
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();

                var _photoRepository = provider.CreatePhotoFingerprint();

                var filecount = _photoRepository.GetAll().Count(n => n.Algorithm == Option.AlgorithmType);
                return filecount;
            }

        }



        //  IList<PhotoFingerprint> Fingerprints;
        protected virtual IList<PhotoFingerprint> ThreadProssSize(IPhotoFingerprintRepository photoFingerprintRepository, int beginindex, int endindex)
        {

        
            //using (var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>())
            {
                //var _photoRepository = provider.CreatePhotoFingerprint();

             //   var take = endindex - beginindex;
                var list = photoFingerprintRepository.GetList(Option.AlgorithmType, beginindex, endindex);


                return list;
            }
        }

        protected override void OnDowrok()
        {
            var count = GetTotalRecord();
            //      ArrayList arr = new ArrayList();
            IList<RangeItem<int>> ranges = new List<RangeItem<int>>();
            for (int i = 0; i < count; i = i + BatchSize)
            {
                var endindex = i + BatchSize;
                if (endindex >= count)
                {
                    endindex = count;
                }
                endindex--;
                ranges.Add(new RangeItem<int>(i, endindex));

            }
            var row = 0;
            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            {
                using (var domainservice = Bootstrap.Currnet.GetService<ISimilarPhotoDomainService>())
                {
                    domainservice.ModuleProvider=provider;
                    var photoFingerprint = provider.CreatePhotoFingerprint();
                    domainservice.Similarity = this.Option.Similarity;

                    foreach (var leftitem in ranges)
                    {
                        var leftlist = ThreadProssSize(photoFingerprint, leftitem.Begin, leftitem.End);
                        domainservice.Fingerprints = leftlist;
                        domainservice.InteriorComparer();
                        for (int i = 0; i < ranges.Count; i++)
                        {
                            if (i <= row) continue;
                            var rangeitem = ranges[i];
                            var reghtlist = ThreadProssSize(photoFingerprint, rangeitem.Begin, rangeitem.End);
                            domainservice.ComparerFingerprints = reghtlist;
                            domainservice.ExternalComparer();
                        }

                        domainservice.ModuleProvider.UnitOfWork.Commit();
                        row++;
                        // arr.Add(list);
                    }

                }
            }
        }
    }
}
