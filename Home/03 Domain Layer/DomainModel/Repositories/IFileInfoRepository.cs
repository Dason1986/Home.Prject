using Library.Domain.Data;
using System.Collections.Generic;
using Home.DomainModel.Aggregates.FileAgg;
using Library.Domain.Data.Repositorys;

namespace Home.DomainModel.Repositories
{
    public interface IFileInfoRepository : IRepository<FileInfo>
    {
        bool FileExists(string filepath);

        FileInfo[] GetFilesByExtensions(string[] extensions,int takes=20);

        bool FileExists(string mD5, long fileSize);

        FileInfo GetByFullPath(string file);

        IDictionary<string, int> GetExtension();

        string[] GetFileDuplicateByMD5();
        FileInfo[] GetFilesByMD5(string md5);
    }
}