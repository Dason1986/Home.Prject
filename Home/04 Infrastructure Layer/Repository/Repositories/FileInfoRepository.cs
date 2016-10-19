using DomainModel.Repositories;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.Data.EF;
using DomainModel.Aggregates.FileAgg;
using System;
using DomainModel.Aggregates.GalleryAgg;

namespace Repository.Repositories
{
    public class FileInfoRepository : Library.Domain.Data.EF.Repository<DomainModel.Aggregates.FileAgg.FileInfo>, IFileInfoRepository
    {
        public FileInfoRepository(EFContext context) : base(context)
        {
        }

        public bool FileExists(string filepath)
        {
            return Set.Any(n => n.FullPath == filepath);
        }

        public bool FileExists(string mD5, long fileSize)
        {
            return Set.Any(n => n.MD5 == mD5&&n.FileSize==fileSize);
        }

        public FileInfo GetByFullPath(string file)
        {
            return Set.FirstOrDefault(n => n.FullPath == file);
        }

        public IEnumerable<FileInfo> GetPhotoFilesByExtensions(string[] extensions)
        {
            return Set.Include("Photo").AsNoTracking().Where(n => extensions.Contains(n.Extension)&& n.StatusCode== Library.ComponentModel.Model.StatusCode.Enabled);
        }
    }
}