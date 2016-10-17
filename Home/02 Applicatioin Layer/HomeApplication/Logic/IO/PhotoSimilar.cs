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
        ISimilarAlgorithm similarImages;
        protected override bool OnVerification()
        {
            switch (Option.AlgorithmType)
            {

                case DomainModel.SimilarAlgorithm.PerceptualHash:
                    similarImages = new PerceptualHash();
                    break;
                case DomainModel.SimilarAlgorithm.GrayHistogram:
                    similarImages = new GrayHistogram();
                    break;

            }

            similarImages.Similarity = this.Option.Similarity;
            return base.OnVerification();
        }
        void Comparer(IPhotoSimilarRepository photoSimilarRepository, PhotoFingerprint leftitem, PhotoFingerprint rightitem)
        {
            var result = similarImages.Compare(leftitem.Fingerprint, rightitem.Fingerprint);
            var isSame = result.IsSame;

            if (isSame)
            {
                if (photoSimilarRepository.Exist(leftitem.PhotoID, rightitem.PhotoID)) return;
                Logger.Info("same:{0} - {1}", leftitem.Owner.File.FileName, rightitem.Owner.File.FileName);
                photoSimilarRepository.Add(new DomainModel.Aggregates.GalleryAgg.PhotoSimilar(CreatedInfo.PhotoSimilar)
                {
                    LeftPhotoID = leftitem.PhotoID,
                    RightPhotoID = rightitem.PhotoID
                });

                photoSimilarRepository.UnitOfWork.Commit();
            }
        }
        void ExternalComparer(IList<PhotoFingerprint> xlist, IList<PhotoFingerprint> ylist)
        {
            try
            {
                Logger.Info("ExternalComparer");
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();

                var photoSimilarRepository = provider.CreatePhotoSimilar();


                foreach (var leftitem in xlist)
                {
                    foreach (var rightitem in ylist)
                    {

                        Comparer(photoSimilarRepository, leftitem, rightitem);
                    }
                }
            }
            catch (Exception ex)
            {

                var message = ExceptionProvider.ProvideFault(ex);
                Logger.Error(ex, string.Format("比較失敗!{0}", message));
            }
        }
        void InteriorComparer(IList<PhotoFingerprint> list)
        {
            try
            {
                Logger.Info("InteriorComparer");
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                var photoSimilarRepository = provider.CreatePhotoSimilar();

                var row = 0;
                foreach (var leftitem in list)
                {

                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == row) continue;
                        var rightitem = list[i];
                        Comparer(photoSimilarRepository, leftitem, rightitem);

                    }
                    row++;
                }
            }
            catch (Exception ex)
            {

                var message = ExceptionProvider.ProvideFault(ex);
                Logger.Error(ex, string.Format("比較失敗!{0}", message));
            }
        }
        //  IList<PhotoFingerprint> Fingerprints;
        protected virtual IList<PhotoFingerprint> ThreadProssSize(int beginindex, int endindex)
        {

            Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
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
            foreach (var leftitem in ranges)
            {
                var leftlist = ThreadProssSize(leftitem.Begin, leftitem.End);
                InteriorComparer(leftlist);
                for (int i = 0; i < ranges.Count; i++)
                {
                    if (i == row) continue;
                    var rightitem = ranges[i];
                    var reghtlist = ThreadProssSize(rightitem.Begin, rightitem.End);
                    ExternalComparer(leftlist, reghtlist);
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
