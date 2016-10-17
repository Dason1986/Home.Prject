using DomainModel.Aggregates.FileAgg;
using DomainModel.Repositories;
using System;

namespace HomeApplication.Services
{

    public class GalleryServiceImpl : ServiceImpl
    {
        public override string ServiceName { get { return "Gallery Service"; } }

        public int GetAllPhotoTotal()
        {
            IPhotoRepository photoRepository = Library.Bootstrap.Currnet.GetService<IPhotoRepository>();
            return photoRepository.GetAllPhotoTotal();
        }

        public int GetPhotoTotalByGallery()
        {
            IPhotoRepository photoRepository = Library.Bootstrap.Currnet.GetService<IPhotoRepository>();
            return photoRepository.GetAllPhotoTotal();
        }

    }

    public class FileManagementServiceImpl : ServiceImpl
    {
        public override string ServiceName { get { return "File Management Service"; } }

        public void CreateFile(string fileInfo)
        {

        }

        public void UploadBigFile(Guid fileid, int position, byte[] fileBuff)
        {

        }

        private void FileUploadCompleted(DomainModel.Aggregates.FileAgg.FileInfo file)
        {
            var process = new FileProcess(file.ID);
            process.CreatePhotoInfo();
            process.Publish();
        }
    }
}