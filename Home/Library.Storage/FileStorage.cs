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

        public virtual bool Exists { get; protected internal set; }

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

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                  
                }

            

                disposedValue = true;
            }
        }


        public void Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}