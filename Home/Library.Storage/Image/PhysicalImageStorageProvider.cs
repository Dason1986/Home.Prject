using System;
using System.IO;

namespace Library.Storage.Image
{

    public class PhysicalImageStorageProvider : IImageStorageProvider
    {
        public PhysicalImageStorageProvider(string path)
        {
            setPath(path);
        }

        public string StoragePath
        {
            get { return _path; }
            protected internal set
            {
                setPath(value);
            }
        }

        bool IFileStorageProvider.CanDelete
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool IFileStorageProvider.CanUpdate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private string rawFormat = ".jpeg";
        private string _path;

        private void setPath(string path)
        {
            if (_path == path) return;
            if (string.Equals(_path, path, StringComparison.OrdinalIgnoreCase)) return;
            _path = path;
        }

        private string GetfilePath(Guid id, int level)
        {
            return Path.Combine(_path, id.ToString(), level.ToString() + rawFormat);
        }

        public Stream Get(Guid id, int level)
        {
            var file = GetfilePath(id, level);
            if (File.Exists(file))
            {
                return File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            throw new FileNotFoundException(file);
        }

        public bool Exist(Guid id, int level)
        {
            return File.Exists(GetfilePath(id, level));
        }

        public void Add(Guid id, Stream stream, int level)
        {
            var file = GetfilePath(id, level);
            var dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (File.Exists(file)) File.Delete(file);
            stream.Seek(0, SeekOrigin.Begin);
            FileStream writer = new FileStream(file, FileMode.CreateNew, FileAccess.ReadWrite);
            stream.CopyTo(writer);
            writer.Close();
            writer.Dispose();
        }

        public byte[] GetRange(Guid id, int index, int size)
        {
            var file = Get(id, 0);
            file.Seek(index, SeekOrigin.Begin);
            byte[] buffer = new byte[size];
            file.Read(buffer, 0, size);
            return buffer;
        }

        void IFileStorageProvider.Delete(Guid id)
        {
            var path = Path.Combine(_path, id.ToString());
            if (Directory.Exists(path)) System.IO.Directory.Delete(path, true);
        }

        Stream IFileStorageProvider.Get(Guid id)
        {
            throw new NotImplementedException();
        }

        FileStorageInfo IFileStorageProvider.GetIndex(Guid id)
        {
            throw new NotImplementedException();
        }

        void IFileStorageProvider.Update(Guid id, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}