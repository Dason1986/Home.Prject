using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Storage.FileEngineProvider.Network
{
    class FTPEntity
    {
        public Limilabs.FTP.Client.Ftp ftp { get; set; }
        public string reomtoDir { get; set; }
        public bool IsLock { get; private set; }

        public void Lock() { IsLock = true; }
        public void Unlock()
        {
            IsLock = false;
        }
    }
 
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
    class FTPPool
    {

        IList<FTPEntity> ftpdics = new List<FTPEntity>();
        static FTPPool()
        {
            if (Current != null) throw new Exception("");
            Current = new FTPPool();
        }
        FTPPool()
        {

        }

        public static FTPPool Current { get; private set; }



        static object lockobj = new object();

        public FTPEntity GetOrAdd(string ftpservice, string ftpuid, string ftppwd, string reomtoDir)
        {
            lock (lockobj)
            {


                var dir = Path.GetDirectoryName(reomtoDir);
                var item = ftpdics.FirstOrDefault(n => n.reomtoDir == dir && !n.IsLock);
                if (item != null)
                {
                    item.Lock();
                    return item;
                }
            
                FTPEntity entiy = new FTPEntity
                {
                    reomtoDir = dir
                };
                Limilabs.FTP.Client.Ftp ftp = new Limilabs.FTP.Client.Ftp();
                ftp.Connect(ftpservice);
                ftp.Login(ftpuid, ftppwd);
                var arry = dir.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arry.Length; i++)
                {
                    ftp.ChangeFolder(arry[i]);
                }
                entiy.ftp = ftp;
                entiy.Lock();
                ftpdics.Add(entiy);
                return entiy;
            }
        }
    }
    public class FTPFileStorage : Library.Storage.IFileStorage, IDisposable
    {
        FTPEntity ftpentity;
        string _filepath;
        public FTPFileStorage(string ftpurl, string ftpuid, string ftppwd, string filepath)
        {
            try
            {


                ftpentity = FTPPool.Current.GetOrAdd(ftpurl, ftpuid, ftppwd, filepath);
                _filepath = Path.GetFileName(filepath);
                Exists = ftpentity.ftp.FileExists(_filepath);


            }
            catch (Exception ex)
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

            var file = ftpentity.ftp.Download(_filepath);
            fs = new MemoryStream(file);
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
                    if (fs != null) fs.Dispose();
                }
                ftpentity.Unlock();
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

