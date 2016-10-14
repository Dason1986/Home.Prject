using DomainModel;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.ModuleProviders;
using Library;
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





            //  using (MainBoundedContext dbcontext = new MainBoundedContext())
            {
                var take = BatchSize;

                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                var _photoRepository = provider.CreatePhoto();
                var photoFingerprintRepository = provider.CreatePhotoFingerprint();

                var photolist = _photoRepository.GetAll().OrderBy(n => n.ID).Skip(beginindex).Take(take).ToList();
                Library.Draw.SimilarImages.PerceptualHash _grayHistogram = new Library.Draw.SimilarImages.PerceptualHash();
                _grayHistogram.Live = Library.Draw.SimilarImages.LiveEnum.Pixels32;
                foreach (var item in photolist)
                {
                    if (photoFingerprintRepository.Exist(item.ID, SimilarAlgorithm.PerceptualHash)) continue;
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
                        Algorithm = SimilarAlgorithm.PerceptualHash,
                        PhotoID = item.ID,
                        //    Owner = item,
                        Fingerprint = fingerprint
                    };
                    photoFingerprintRepository.Add(photoFingerprint);
                    provider.UnitOfWork.Commit();
                    image.Dispose();



                    #endregion
                }
            }
        }
    }
}