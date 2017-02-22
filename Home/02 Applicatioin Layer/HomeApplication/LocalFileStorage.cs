using System;
using System.IO;

namespace HomeApplication
{

    public class LocalFileStorage : Library.Storage.IFileStorage
    {
        public LocalFileStorage(string filepath)
        {
            fileinfo = new FileInfo(filepath);
            Exists = fileinfo.Exists;
        }
        System.IO.FileInfo fileinfo;
        public bool Exists { get; set; }

        public Guid ID { get; set; }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public Stream Get()
        {
            return fileinfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        public byte[] GetRange(int index, int size)
        {
            throw new NotImplementedException();
        }
    }
}