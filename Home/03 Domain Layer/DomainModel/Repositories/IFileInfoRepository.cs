﻿using DomainModel.Aggregates.FileAgg;
using Library.Domain.Data;
using System.Collections.Generic;

namespace DomainModel.Repositories
{
    public interface IFileInfoRepository : IRepository<FileInfo>
    {
        bool FileExists(string filepath);
        IEnumerable<FileInfo> GetFilesByExtensions(string[] extensions);
    }
}