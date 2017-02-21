using Home.DomainModel.ModuleProviders;
using Library;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using Home.Repository;
using Home.Repository.ModuleProviders;
using System.Collections.Generic;
using System.Linq;
using HomeApplication.DomainServices;
using System.IO;

namespace HomeApplication.Logic.IO
{
    public class DeleteFileDistinctByMD5 : BaseLogicService
    {
        private readonly IPhotoEnvironment _photoEnvironment = new PhotoEnvironment();
        private EmptyOption _option;

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

        protected override void OnDowrok()
        {
            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();

            var fileInfoRepository = provider.CreateFileInfo();
            var photoRepository = provider.CreatePhoto();
            var md5S = fileInfoRepository.GetFileDistinctByMD5();
            if (md5S.Length == 0) return;
            _photoEnvironment.LoadConfig(provider.CreateSystemParameter());
            // var _photoAttRepository = provider.CreatePhotoAttribute();

            foreach (var item in md5S)
            {
                var fileDistincts = fileInfoRepository.GetAll().Where(n => n.MD5 == item).ToList();
                if (fileDistincts.Count <= 1) continue;

                for (int i = 1; i < fileDistincts.Count; i++)
                {
                    var fileinfo = fileDistincts[i];
                    Logger.TraceByContent("Distinct", fileinfo.FileName);

                    var photo = fileinfo.Photo ?? photoRepository.GetByFileId(fileinfo.ID);
                    if (photo == null)
                    {
                        fileInfoRepository.Remove(fileinfo);
                    }
                    else
                    {
                        Logger.Trace("删除文件！");
                        DeleFile(fileinfo);

                        photoRepository.DeletePhotoAllInfoByID(photo.ID);
                        fileInfoRepository.Remove(fileinfo);
                        try
                        {
                            System.IO.File.Delete(fileinfo.FileName);
                        }
                        catch (System.Exception)
                        {
                            Logger.WarnByContent("删除文件失败！", fileinfo.FileName);
                        }
                    }
                    provider.UnitOfWork.Commit();
                }
            }
        }

        private void DeleFile(Home.DomainModel.Aggregates.FileAgg.FileInfo fileinfo)
        {
            try
            {
                var filestorage = _photoEnvironment.CreateImageStorage(fileinfo.ID);
                filestorage.Delete();
            }
            catch (System.Exception)
            {
                Logger.WarnByContent("删除文件失败！", fileinfo.FileName);
            }
        }
    }
}