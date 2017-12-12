using System;
using System.IO;

namespace HomeApplication
{

    public class LocalFileStorage : Library.Storage.IFileStorage, IDisposable
    {
        public LocalFileStorage(string filepath)
        {
            fileinfo = new FileInfo(filepath);
            Exists = fileinfo.Exists;
        }
        System.IO.FileInfo fileinfo;
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
            fileinfo.Delete();
        }

        public Stream Get()
        {
            if (!Exists) throw new FileNotFoundException();
            if (fs != null) return fs;

            fs = fileinfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return fs;
        }

        public byte[] GetRange(int index, int size)
        {
            Get();

            fs.Seek(index, SeekOrigin.Begin);
            byte[] buffer = new byte[size];
            fs.Read(buffer, 0, size);
            return buffer;
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

                fs = null;

                disposedValue = true;
            }
        }


        void IDisposable.Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public long Size()
        {
            return fileinfo.Length;
        }
        #endregion
    }
}