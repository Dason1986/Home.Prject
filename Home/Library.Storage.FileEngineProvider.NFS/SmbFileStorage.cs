using System;
using System.Collections.Generic;
using System.IO;

namespace Library.Storage.FileEngineProvider.Network
{
    class SBMPool
    {

        IList<string> ftpdics = new List<string>();
        static SBMPool()
        {
            if (Current != null) throw new Exception("");
            Current = new SBMPool();
        }
        SBMPool()
        {

        }

        public static SBMPool Current { get; private set; }



        static object lockobj = new object();
       
        public void GetOrAdd(string ftpservice, string ftpuid, string ftppwd)
        {
            lock (lockobj)
            {
                if (ftpdics.Contains(ftpservice)) return;
                NetworkShareAccesser.DisconnectFromShare(@"\\"+ ftpservice);
                NetworkShareAccesser.Access(ftpservice, ftpuid, ftppwd);
                ftpdics.Add(ftpservice);

            }
        }
    }
    public class SmbFileStorage : IFileStorage, IDisposable
    {

        string _filepath; 
        public SmbFileStorage(string ftpurl, string ftpuid, string ftppwd, string filepath)
        {
            try
            {
                SBMPool.Current.GetOrAdd(ftpurl, ftpuid, ftppwd);
                _filepath =@"\\"+Path.Combine( ftpurl,filepath);
               


                Exists = File.Exists(_filepath);


            }
            catch (Exception ex)
            {

                throw ex;
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
            fs = File.Open(_filepath, FileMode.Open,FileAccess.Read);
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
            throw new NotImplementedException();
        }
        #endregion
    }
}