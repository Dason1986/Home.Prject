using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.Repositories;
using HomeApplication.Dtos;
using System;

namespace HomeApplication.Services
{

    public class FileManagementServiceImpl : ServiceImpl
    {
        public override string ServiceName { get { return "File Management Service"; } }
        protected readonly IFileInfoRepository FileInfoRepository;
        public FileManagementServiceImpl(IFileInfoRepository fileInfoRepository)
        {
            FileInfoRepository = fileInfoRepository;
        }
        public void CreateFile(FileInfoDto file)
        {

        }

        public void UploadBigFile(Guid fileid, int position, byte[] fileBuff)
        {

        }

        private void FileUploadCompleted(FileInfo file)
        {

            var process = new FileAggregateRoot(file, FileInfoRepository);
            if (process.IsImageFile())
                process.PublishPhotoDomain();
        }
    }
}