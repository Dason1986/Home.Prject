using System;
using System.IO;

namespace Library.Storage.FileEngineProvider.EF
{
    public class EFStorageProvider : IFileStorageProvider
    {
        public EFStorageProvider()
        {
        }

        public bool CanDelete
        {
            get
            {
                return false;
            }
        }

        public bool CanUpdate
        {
            get
            {
                return false;
            }
        }

        public long Size => throw new NotImplementedException();

        public void Delete(Guid iD)
        {
            throw new NotImplementedException();
        }

        public Stream Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Stream Get(Guid id, int version)
        {
            throw new NotImplementedException();
        }

        public FileStorageInfo GetIndex(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid iD, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}