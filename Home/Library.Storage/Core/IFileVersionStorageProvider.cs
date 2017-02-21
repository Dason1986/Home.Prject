using System;
using System.IO;

namespace Library.Storage
{

    public interface IFileVersionStorageProvider : IFileStorageProvider
    {
        int[] GetVersions();

        Stream Get(Guid id, int version);
    }
}