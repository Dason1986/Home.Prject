using System;
using System.IO;

namespace Library.Storage
{

    public class FileStorage : IFileStorage
    {
        protected internal FileStorageInfo Index { get; set; }
        protected internal IFileStorageProvider Provider { get; set; }
        public Guid ID { get; private set; }

        public bool CanDelete { get { return Provider.CanDelete; } }
        public bool CanUpdate { get { return Provider.CanUpdate; } }

        public FileStorage(IFileStorageProvider provider, Guid fileid)
        {
            ID = fileid;
            Provider = provider;
            //  Index = provider.GetIndex(fileid);
        }

        public Stream Get()
        {
            return Provider.Get(ID);
        }

        public virtual void Update(Stream stream)
        {
            Provider.Update(ID, stream);
        }

        public void Delete()
        {
            Provider.Delete(ID);
        }

        public virtual byte[] GetRange(int index, int size)
        {
            throw new NotImplementedException();
        }
    }
}