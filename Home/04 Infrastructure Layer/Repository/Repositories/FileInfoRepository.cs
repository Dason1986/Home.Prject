using Home.DomainModel.Repositories;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.Data.EF;
using Home.DomainModel.Aggregates.FileAgg;
using System;
using Home.DomainModel.Aggregates.GalleryAgg;
using System.Data.Entity;

namespace Home.Repository.Repositories
{
    public class FileInfoRepository : Library.Domain.Data.EF.Repository<FileInfo>, IFileInfoRepository
    {
        public FileInfoRepository(DbContext context) : base(context)
        {
        }

        public bool FileExists(string filepath)
        {
            return Wrapper.Find().Any(n => n.FullPath == filepath);
        }

        public bool FileExists(string mD5, long fileSize)
        {
            if (string.IsNullOrEmpty(mD5)) return false;
            return Wrapper.Find().Any(n => n.MD5 == mD5 && n.FileSize == fileSize);
        }

        public FileInfo GetByFullPath(string file)
        {
            return Wrapper.Find().Include("Photo").FirstOrDefault(n => n.FullPath == file);
        }
        public IDictionary<string, int> GetExtension()
        {
            var list = Wrapper.Find().Where(n=> n.StatusCode == Library.ComponentModel.Model.StatusCode.Enabled).GroupBy(n => n.Extension ).Select(n => new { Name = n.Key, Count = n.Count() }).ToArray();
            var dic = new Dictionary<string, int>();
            foreach (var item in list)
            {
                dic.Add(item.Name, item.Count);
            }
            return dic;
        }
        public string[] GetFileDuplicateByMD5()
        {
            var md5s = this.UnitOfWork.ExecuteQuery<string>("select  md5   from  DuplicateByMD5View ").ToArray();
            // var md5s = Wrapper.Find().GroupBy(n => n.MD5).Select(n => new { MD5 = n.Key, Count = n.Count() }).Where(n => n.Count > 1).Select(n => n.MD5).ToArray();
            //select  md5 ,count(0) 'count'  from  fileinfo  group  by  md5  having  count(md5) > 1 ORDER BY count desc
            return md5s;

        }

        public FileInfo[] GetFilesByMD5(string md5)
        {  return Wrapper.Find().Include("Photo").Where(n =>  n.StatusCode == Library.ComponentModel.Model.StatusCode.Enabled && n.MD5 == md5).OrderBy(n => n.Created).ToArray();
            
        }

        public FileInfo[] GetPhotoFilesByExtensions(string[] extensions, int takes = 5)
        {
            return Wrapper.Find().Include("Photo").Where(n => extensions.Contains(n.Extension) && n.StatusCode == Library.ComponentModel.Model.StatusCode.Enabled && (n.MD5 == null || n.MD5 == "")).Take(takes).ToArray();
        }
    }
}