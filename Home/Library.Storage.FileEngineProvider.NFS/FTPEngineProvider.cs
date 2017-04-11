using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Storage.FileEngineProvider.Network
{
    public class FTPEngineProvider : IFileStorageProvider
    {
        public bool CanDelete
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CanUpdate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Stream Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public FileStorageInfo GetIndex(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Stream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class FTPFileStorage : Library.Storage.IFileStorage, IDisposable
    {
        public FTPFileStorage(string ftpurl, string ftpuid, string ftppwd, string filepath)
        {
            try
            {
                Limilabs.FTP.Client.Ftp ftp = new Limilabs.FTP.Client.Ftp();
              
                ftp.Connect(ftpurl);
                ftp.Login(ftpuid, ftppwd);
                var file = ftp.Download(filepath);
                fs = new MemoryStream(file);
                Exists = true;
            }
            catch (Exception)
            {

                throw;
            }


        }

        Stream fs;
        public bool Exists { get; set; }

        public Guid ID { get; set; }

        public void Delete()
        {
            if (!Exists) return;
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
                fs = null;
            }
           
        }

        public Stream Get()
        {
           
            return fs;
        }

        public byte[] GetRange(int index, int size)
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
                    if (fs == null) fs.Dispose();
                }

                fs = null;

                disposedValue = true;
            }
        }


        void IDisposable.Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

