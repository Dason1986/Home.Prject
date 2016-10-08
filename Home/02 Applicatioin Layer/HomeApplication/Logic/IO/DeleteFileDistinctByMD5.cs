using DomainModel.ModuleProviders;
using Library;
using Repository;
using Repository.ModuleProviders;
using System.Collections.Generic;
using System.Linq;

namespace HomeApplication.Logic.IO
{
    public class DeleteFileDistinctByMD5 : BaseMultiThreadingLogicService
    {
      

        EmptyOption _option;
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


        protected override int GetTotalRecord()
        {
            
            {
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                var _photoRepository = provider.CreateFileInfo();

                md5s = _photoRepository.GetAll().GroupBy(n => n.MD5).Where(n => n.Count() > 1).Select(n => n.Key).ToList();
                return md5s.Count;
            }
        }
        IList<string> md5s;
        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            Logger.Info(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
           // using (MainBoundedContext dbcontext = new MainBoundedContext())
            {
                var take = endindex - beginindex;
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                var _fileInfoRepository = provider.CreateFileInfo();
                var _photoRepository = provider.CreatePhoto();
                var _photoAttRepository = provider.CreatePhotoAttribute();
                var photolist = md5s.Skip(beginindex).Take(take).ToList();
                foreach (var item in photolist)
                {
                    var fileDistincts = _fileInfoRepository.GetAll().Where(n => n.MD5 == item).OrderByDescending(n => n.StatusCode).ToList();
                    if (fileDistincts.Count <= 1) continue;

                    for (int i = 1; i < fileDistincts.Count; i++)
                    {
                        var fileinfo = fileDistincts[i];
                        if (fileinfo.StatusCode != Library.ComponentModel.Model.StatusCode.Enabled) continue;

                        // fileinfo.StatusCode = Library.ComponentModel.Model.StatusCode.Disabled;
                        if (fileinfo.Photo != null)
                        {
                            _photoRepository.DeletePhotoAllInfoByID(fileinfo.Photo.ID);


                        }
                        _fileInfoRepository.Get(fileinfo.ID).StatusCode = Library.ComponentModel.Model.StatusCode.Disabled;
                        provider.UnitOfWork.Commit();
                    }



                }
            }
        }
    }
}