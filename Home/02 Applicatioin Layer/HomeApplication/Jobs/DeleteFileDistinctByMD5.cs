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
using Quartz;

namespace HomeApplication.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class DeleteFileDistinctByMD5 : ScheduleJobProvider
    {
        readonly static object _sync = new object();
        public override void Execute(IJobExecutionContext context)
        {
            lock (_sync)
            {

                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();

                var fileInfoRepository = provider.CreateFileInfo();
                var photoRepository = provider.CreatePhoto();
                var photoattr = provider.CreatePhotoAttribute();
                var photoface = provider.CreatePhotoFaces();
                var md5S = fileInfoRepository.GetFileDuplicateByMD5();
                if (md5S.Length == 0) return;
                var photoEnvironment = Library.Bootstrap.Currnet.GetService<IPhotoEnvironment>();
                photoEnvironment.LoadConfig(provider.CreateSystemParameter());
                foreach (var item in md5S)
                {
                    var files = fileInfoRepository.GetFilesByMD5(item);
                    var onefile = files[0];
                    for (int i = 1; i < files.Length; i++)
                    {
                        var file = files[i];
                        using (var storage = file.GetStorage())
                        {
                            if (storage.Exists)
                            {
                                storage.Delete();
                            }
                        }
                        var imagestorage = photoEnvironment.CreateImageStorage(file.ID);
                        imagestorage.Delete();
                        file.StatusCode = Library.ComponentModel.Model.StatusCode.Delete;
                        file.FileStatue = Home.DomainModel.Aggregates.FileAgg.FileStatue.Duplicate;
                        var photo = photoRepository.GetByFileId(file.ID);
                        if (photo != null)
                        {
                            if (photo.Attributes != null && photo.Attributes.Count > 0)
                            {
                                foreach (var attitemID in photo.Attributes.Select(n=>n.ID).ToArray())
                                {
                                    photoattr.Remove(attitemID);
                                }

                            }
                            if (photo.Faces != null && photo.Faces.Count > 0)
                            {
                                foreach (var attitemID in photo.Faces.Select(n => n.ID).ToArray())
                                {
                                    photoface.Remove(attitemID);
                                }

                            }
                            photoRepository.Remove(photo.ID);
                        }
                        provider.UnitOfWork.Commit();
                    }
                }
            }
        }

        public override void Initialize()
        {

        }
    }
}