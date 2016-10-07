using Repository;
using Repository.ModuleProviders;
using System;
using System.Linq;
using Library.Draw.SimilarImages;
using DomainModel.Aggregates.GalleryAgg;
using System.Collections.Generic;
using DomainModel.ModuleProviders;
using Library;

namespace HomeApplication.Logic.IO
{
    public class PhotoSimilarOption : IOption
    {
        public double Similarity { get; set; }

        public DomainModel.SimilarAlgorithm AlgorithmType { get; set; }
    }
    public class PhotoSimilar : BaseMultiThreadingLogicService
    {
        public PhotoSimilar()
        {
            BatchSize = 5;

            ThreadCount = 2;
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




        protected override int GetTotalRecord()
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
        //  IList<PhotoFingerprint> Fingerprints;
        protected override void ThreadProssSize(int beginindex, int endindex)
        {

            Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);

            try
            {
               
                {

                    var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                    var _photoRepository = provider.CreatePhotoFingerprint();

                    var take = endindex - beginindex;
                    var list = _photoRepository.GetAll().OrderBy(n => n.ID).Skip(beginindex).Take(take).ToList();
                    var _photoSimilarRepository = provider.CreatePhotoSimilar();

                    foreach (var leftitem in list)
                    {

                        Logger.Info(leftitem.Owner.File.FileName);
                        try
                        {

                            var Fingerprints = _photoRepository.GetAll().OrderBy(n => n.ID).Where(n => n.Algorithm == Option.AlgorithmType);
                            foreach (var rightitem in Fingerprints)
                            {
                                if (leftitem.ID == rightitem.ID) continue;
                                // if (leftitem.Algorithm != rightitem.Algorithm) continue;

                                var result = similarImages.Compare(leftitem.Fingerprint, rightitem.Fingerprint);
                                var isSame = result.IsSame;

                                if (isSame)
                                {
                                    if (_photoSimilarRepository.Exist(leftitem.PhotoID, rightitem.PhotoID)) continue;
                                    Logger.Info("same:{0} - {1}", leftitem.Owner.File.FileName, rightitem.Owner.File.FileName);
                                    _photoSimilarRepository.Add(new DomainModel.Aggregates.GalleryAgg.PhotoSimilar(CreatedInfo.PhotoSimilar)
                                    {
                                        LeftPhotoID = leftitem.PhotoID,
                                        RightPhotoID = rightitem.PhotoID
                                    });
                                    provider.UnitOfWork.Commit();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            var message = ExceptionProvider.ProvideFault(ex);
                            Logger.Error(ex, string.Format("file:{0} ,比較失敗!{1}", leftitem.Owner.File.FileName, message));
                        }
                    }


                }
            }
            catch (Exception ex)
            {

                var message = ExceptionProvider.ProvideFault(ex);
                Logger.Error(ex, string.Format("{0}-{1}比較失敗!{2}", beginindex, endindex, message));
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
