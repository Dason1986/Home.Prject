using Home.DomainModel.Repositories;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.Data.EF;
using Home.DomainModel.Aggregates.FileAgg;
using System;
using Home.DomainModel.Aggregates.GalleryAgg;

namespace Home.Repository.Repositories
{
    public class FileInfoRepository : Library.Domain.Data.EF.Repository<FileInfo>, IFileInfoRepository
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
            return Set.Any(n => n.MD5 == mD5 && n.FileSize == fileSize);
        }

        public FileInfo GetByFullPath(string file)
        {
            return Set.Include("Photo").AsNoTracking().FirstOrDefault(n => n.FullPath == file);
        }

        public string[] GetFileDistinctByMD5()
        {
            var md5s = Set.GroupBy(n => n.MD5).Select(n => new { MD5 = n.Key, Count = n.Count() }).Where(n => n.Count > 1).Select(n => n.MD5).ToArray();

            return md5s;
            // return 	EfContext.Database.SqlQuery<string>("SELECT md5 FROM (SELECT MD5,count(0) 'aa' FROM fileinfo GROUP BY MD5) t1 WHERE aa > 1").ToList();
        }

        public IEnumerable<FileInfo> GetPhotoFilesByExtensions(string[] extensions)
        {
            return Set.Include("Photo").AsNoTracking().Where(n => extensions.Contains(n.Extension) && n.StatusCode == Library.ComponentModel.Model.StatusCode.Enabled && n.Photo == null);
        }
    }
}