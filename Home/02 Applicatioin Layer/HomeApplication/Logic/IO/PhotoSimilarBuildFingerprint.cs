using DomainModel;
using DomainModel.Aggregates.GalleryAgg;
using Repository;
using Repository.ModuleProviders;
using System;
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
                return Option;
            }

            set
            {
                Option = (EmptyOption)value;
            }
        }
        public EmptyOption Option { get; set; }


        protected override int GetTotalRecord()
        {
            using (MainBoundedContext dbcontext = new MainBoundedContext())
            {
                GalleryModuleProvider provider = new GalleryModuleProvider(dbcontext);
                var _photoRepository = provider.CreatePhoto();

                var filecount = _photoRepository.GetAll().Count();
                return filecount;
            }
        }

        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);





            using (MainBoundedContext dbcontext = new MainBoundedContext())
            {
                var take = endindex - beginindex;

                GalleryModuleProvider provider = new GalleryModuleProvider(dbcontext);
                var _photoRepository = provider.CreatePhoto();
                var photoFingerprintRepository = provider.CreatePhotoFingerprint();

                var photolist = _photoRepository.GetAll().OrderBy(n => n.ID).Skip(beginindex).Take(take).ToList();
                Library.Draw.SimilarImages.GrayHistogram _grayHistogram = new Library.Draw.SimilarImages.GrayHistogram();
                _grayHistogram.Live = Library.Draw.SimilarImages.LiveEnum.Pixels256;
                foreach (var item in photolist)
                {
                    if (photoFingerprintRepository.Exist(item.ID, SimilarAlgorithm.GrayHistogram)) continue;
                    Logger.Info(item.File.FileName);
                    #region MyRegion
                    Image image = null;
                    try
                    {
                        image = Image.FromFile(item.File.FullPath);
                        if (image.Width < 256) continue;
                    }
                    catch (Exception ex)
                    {
                        // Console.WriteLine(ex);
                        Logger.Error(ex, item.File.FullPath + "文件不能以图像形式打开！");
                        continue;

                    }

                    var fingerprint = _grayHistogram.BuildFingerprint(image);
                    var photoFingerprint = new PhotoFingerprint(CreatedInfo.BuildFingerprint)
                    {
                        Algorithm = SimilarAlgorithm.GrayHistogram,
                        PhotoID = item.ID,
                        //    Owner = item,
                        Fingerprint = fingerprint
                    };
                    photoFingerprintRepository.Add(photoFingerprint);
                    provider.UnitOfWork.Commit();




                    #endregion
                }
            }
        }
    }
}