using Library.Domain.Data;
using System.Collections.Generic;
using Home.DomainModel.Aggregates.FileAgg;
using Library.Domain.Data.Repositorys;

namespace Home.DomainModel.Repositories
{
    public interface IFileInfoRepository : IRepository<FileInfo>
    {
        bool FileExists(string filepath);

        FileInfo[] GetPhotoFilesByExtensions(string[] extensions,int takes);

        bool FileExists(string mD5, long fileSize);

        FileInfo GetByFullPath(string file);

        IDictionary<string, int> GetExtension();

        string[] GetFileDuplicateByMD5();
    }
}