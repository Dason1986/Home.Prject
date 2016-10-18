using Repository;
using Repository.ModuleProviders;
using System;
using System.Linq;
using Library.Draw.SimilarImages;
using DomainModel.Aggregates.GalleryAgg;
using System.Collections.Generic;
using DomainModel.ModuleProviders;
using Library;
using System.Collections;
using DomainModel.Repositories;
using Library.ComponentModel.Logic;
using DomainModel.DomainServices;
using Library.Infrastructure.Application;

namespace HomeApplication.Logic.IO
{
    public class PhotoSimilarOption : IOption
    {
        public double Similarity { get; set; }

        public DomainModel.SimilarAlgorithm AlgorithmType { get; set; }
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

                var filecount = _photoRepository.GetAll().Where(n => n.Algorithm == Option.AlgorithmType).Count();
                return filecount;
            }

        }
       
       

    
        //  IList<PhotoFingerprint> Fingerprints;
        protected virtual IList<PhotoFingerprint> ThreadProssSize(int beginindex, int endindex)
        {

            Logger.Trace(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            var _photoRepository = provider.CreatePhotoFingerprint();

            var take = endindex - beginindex;
            var list = _photoRepository.GetAll().Where(n => n.Algorithm == Option.AlgorithmType).OrderBy(n => n.ID).Skip(beginindex).Take(take).ToList();


            return list;
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
                ranges.Add(new RangeItem<int>(i, endindex));

            }
            var row = 0;
            var domainservice = Bootstrap.Currnet.GetService<ISimilarPhotoDomainService>();
            domainservice.ModuleProvider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
       
            domainservice.Similarity = this.Option.Similarity;
       
            foreach (var leftitem in ranges)
            {
                var leftlist = ThreadProssSize(leftitem.Begin, leftitem.End);
                domainservice.Fingerprints = leftlist;
                domainservice.InteriorComparer();
                for (int i = 0; i < ranges.Count; i++)
                {
                    if (i == row) continue;
                    var rightitem = ranges[i];
                    var reghtlist = ThreadProssSize(rightitem.Begin, rightitem.End);
                    domainservice.ComparerFingerprints = reghtlist;
                    domainservice. ExternalComparer();
                }
                row++;
                // arr.Add(list);
            }
        }
    }
    public class PhotoSimilarOptionCommandBuilder : IOptionCommandBuilder<PhotoSimilarOption>
    {
        public PhotoSimilarOption GetOption()
        {
            return _option;
        }
        PhotoSimilarOption _option;
        IOption IOptionCommandBuilder.GetOption()
        {
            return _option;
        }
        public void RumCommandLine()
        {
            _option = new PhotoSimilarOption();
            Console.Write("是否使用默认条件（Y）：");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                _option.AlgorithmType = DomainModel.SimilarAlgorithm.PerceptualHash;
                _option.Similarity = 5;

                return;
            }
            Console.WriteLine();
            LabCmd:
            Console.Write("輸入圖像正確率：");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("不能爲空");
                goto LabCmd;
            }
            var dimilarity = Library.HelperUtility.StringUtility.TryCast<double>(path);
            if (dimilarity.HasError)
            {
                Console.WriteLine("無效輸入");
                goto LabCmd;
            }
            if (dimilarity.Value > 100 || dimilarity.Value < 50)
            {
                Console.WriteLine("正確率不能少於50並大於100");
                goto LabCmd;
            }
            _option.Similarity = dimilarity.Value;

        }
    }
}
