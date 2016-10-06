using Repository;
using Repository.ModuleProviders;
using Repository.Repositories;
using System;
using System.Linq;

namespace HomeApplication.Logic.IO
{
    public class PhotoSimilarOption : IOption
    {
        public decimal Similarity { get; set; }
    }
    public class PhotoSimilar : BaseMultiThreadingLogicService
    {
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
        public PhotoSimilarOption Option { get; set; }

        protected override int GetTotalRecord()
        {

            using (MainBoundedContext dbcontext = new MainBoundedContext())
            {
                GalleryModuleProvider provider = new GalleryModuleProvider(dbcontext);

                var _photoRepository = provider.CreatePhotoFingerprint();

                var filecount = _photoRepository.GetAll().Count();
                return filecount;
            }

        }

        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            using (MainBoundedContext dbcontext = new MainBoundedContext())
            {
                Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
                Library.Draw.SimilarImages.GrayHistogram _grayHistogram = new Library.Draw.SimilarImages.GrayHistogram();
                GalleryModuleProvider provider = new GalleryModuleProvider(dbcontext);
                var _photoRepository = provider.CreatePhotoFingerprint();

                var take = endindex - beginindex;
                var list = _photoRepository.GetAll().OrderBy(n => n.ID).Skip(beginindex).Take(take).ToList();

                MainBoundedContext dbcontexComper = new MainBoundedContext();
                Library.Domain.Data.EF.UnitOfWork unitOfWork = new Library.Domain.Data.EF.UnitOfWork(dbcontexComper);
                var _photoSimilarRepository = new PhotoSimilarRepository(dbcontexComper);
                foreach (var leftitem in list)
                {
                    foreach (var rightitem in _photoRepository.GetAll())
                    {
                        if (leftitem.ID == rightitem.ID) continue;
                        if (_photoSimilarRepository.Exist(leftitem.PhotoID, rightitem.PhotoID)) continue;
                        var result = _grayHistogram.Compare(leftitem.Fingerprint, rightitem.Fingerprint);
                        if (result.IsSame)
                        {
                            _photoSimilarRepository.Add(new DomainModel.Aggregates.GalleryAgg.PhotoSimilar(CreatedInfo.PhotoSimilar)
                            {
                                LeftPhotoID = leftitem.PhotoID,
                                RightPhotoID = rightitem.PhotoID
                            });

                        }
                    }
                    unitOfWork.Commit();
                }


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
                _option.Similarity = 80; ;

                return;
            }
            LabCmd:
            Console.Write("輸入圖像正確率：");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("不能爲空");
                goto LabCmd;
            }
            var dimilarity = Library.HelperUtility.StringUtility.TryCast<decimal>(path);
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
