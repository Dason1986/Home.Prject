using System;
using System.IO;

namespace Library.Storage.Image
{

    public interface IImageStorageProvider : IFileStorageProvider
    {
        Stream Get(Guid ID, int level);

        bool Exist(Guid ID, int level);

        void Add(Guid ID, Stream stream, int level);

        byte[] GetRange(Guid iD, int index, int size);
    }
}