using DomainModel.Aggregates.FileAgg;
using HomeApplication.Dto;
using System;

namespace HomeApplication.Services
{

    public class FileManagementServiceImpl : ServiceImpl
    {
        public override string ServiceName { get { return "File Management Service"; } }

        public void CreateFile(FileInfoDto file)
        {

        }

        public void UploadBigFile(Guid fileid, int position, byte[] fileBuff)
        {

        }

        private void FileUploadCompleted(FileInfoDto file)
        {
            var process = new FileAggregateRoot(file.ID);
            process.CreatePhotoInfo();
            process.Publish();
        }
    }
}