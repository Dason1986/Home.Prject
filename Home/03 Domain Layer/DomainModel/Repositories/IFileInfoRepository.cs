using DomainModel.Aggregates.FileAgg;
using Library.Domain.Data;
using System.Collections.Generic;
using System;

namespace DomainModel.Repositories
{
    public interface IFileInfoRepository : IRepository<FileInfo>
    {
        bool FileExists(string filepath);
        IEnumerable<FileInfo> GetPhotoFilesByExtensions(string[] extensions);
        bool FileExists(string mD5, long fileSize);
        FileInfo GetByFullPath(string file);
		IList<string> GetFileDistinctByMD5();
	}
}