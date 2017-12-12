using System;
using System.IO;

namespace Library.Storage
{

    public interface IFileStorageProvider
    {
        bool CanDelete { get; }
        bool CanUpdate { get; }
        long Size { get; }

        void Delete(Guid id);

        Stream Get(Guid id);

        FileStorageInfo GetIndex(Guid id);

        void Update(Guid id, Stream stream);
    }
}