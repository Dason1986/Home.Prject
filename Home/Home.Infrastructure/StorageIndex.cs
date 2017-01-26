using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace Library.Storage
{
    /// <summary>
    ///
    /// </summary>
    public interface IFileIndexServiceProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IQueryable<FileStorageInfo> CreateSet();
    }

    /// <summary>
    ///
    /// </summary>
    public interface IFileStorageServiceProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FileStorage Get(Guid id);
    }

    /// <summary>
    ///
    /// </summary>
    public interface IStorageStory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FileStorage GetFileStorage(Guid id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FileVersionStorage GetFileVerionStorage(Guid id);
    }

    /// <summary>
    ///
    /// </summary>
    public interface IFileStorageBlock
    {
        int BlockSize { get; }
    }

    [Serializable]
    public class FileStorageNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public FileStorageNotFoundException()
        {
        }

        public FileStorageNotFoundException(string message) : base(message)
        {
        }

        public FileStorageNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FileStorageNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class FileStorageInfoNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public FileStorageInfoNotFoundException()
        {
        }

        public FileStorageInfoNotFoundException(string message) : base(message)
        {
        }

        public FileStorageInfoNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FileStorageInfoNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class StorageStoryContext
    {
        protected StorageStoryContext(IFileIndexServiceProvider indexProvider, IFileStorageServiceProvider storageProvider)
        {
            _indexProvider = indexProvider;
            _storageProvider = storageProvider;
        }

        private readonly IFileIndexServiceProvider _indexProvider;
        private readonly IFileStorageServiceProvider _storageProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual FileStorageInfo GetFile(Guid id)
        {
            IQueryable<FileStorageInfo> set = _indexProvider.CreateSet();
            return set.FirstOrDefault(n => n.ID == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public virtual FileStorageInfo GetFile(string filename)
        {
            IQueryable<FileStorageInfo> set = _indexProvider.CreateSet();
            return set.FirstOrDefault(n => n.Name == filename);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="stratindex"></param>
        /// <param name="endindex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        protected internal FileStorageInfo[] GetFiles(Func<FileStorageInfo, bool> predicate, int stratindex, int endindex, out long total)
        {
            IQueryable<FileStorageInfo> set = _indexProvider.CreateSet();
            var source = set.Where(predicate);
            total = source.Count();
            var take = endindex - stratindex;
            var items = source.Skip(stratindex).Take(take).ToArray();
            return items;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public byte[] GetFileBytes(string filename)
        {
            var stream = GetFileStream(filename);

            var reader = new BinaryReader(stream);
            byte[] buffer = reader.ReadBytes((int)stream.Length);
            return buffer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] GetFileBytes(Guid id)
        {
            var stream = GetFileStream(id);

            var reader = new BinaryReader(stream);
            byte[] buffer = reader.ReadBytes((int)stream.Length);
            return buffer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Stream GetFileStream(string filename)
        {
            var fileinfo = GetFile(filename);
            if (fileinfo == null) throw new FileStorageInfoNotFoundException("", new FileNotFoundException("", filename));
            FileStorage storage = _storageProvider.Get(fileinfo.ID);
            return storage.Get();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Stream GetFileStream(Guid id)
        {
            var fileinfo = GetFile(id);
            if (fileinfo == null) throw new FileStorageInfoNotFoundException("");
            FileStorage storage = _storageProvider.Get(fileinfo.ID);
            return storage.Get();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public virtual FileStorage CreateFile(Guid id, string filename)
        {
            var info = FileStorageInfo.Create(filename);
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>
        ///
        /// </summary>
        Guid ID { get; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Stream Get();

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        byte[] GetRange(int index, int size);
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class StorageInfo
    {
        /// <summary>
        ///
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        internal const int NameSizeBuffer = 100;
    }

    public class FileStorage
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
    }

    public class FileVersionStorage : FileStorage
    {
        public FileVersionStorage(IFileVersionStorageProvider provider, Guid fileid) : base(provider, fileid)
        {
            VersionProvider = provider;
        }

        protected internal IFileVersionStorageProvider VersionProvider { get; set; }

        public int[] GetVersions()
        {
            throw new NotImplementedException();
        }

        public Stream Get(int version)
        {
            return VersionProvider.Get(Index.ID, version);
        }

        public override void Update(Stream stream)
        {
            //            Index.Versoin;
            throw new NotImplementedException();
        }
    }

    public class FileStorageInfo : StorageInfo
    {
        public DateTime Modified { get; set; }

        public static FileStorageInfo Create(string name)
        {
            return new FileStorageInfo { Name = name, Created = DateTime.Now, ID = Guid.NewGuid() };
        }
    }

    public class DirectoryStorageInfo : StorageInfo
    {
    }

    public static class FileStoryHelper
    {
        public static void InitPath(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public static string InitPath(string galleryPath, Guid id)
        {
            var path = Path.Combine(galleryPath, id.ToString());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }
    }

    public class SingleLayerFileStory : IStorageStory
    {
        public SingleLayerFileStory(IFileStorageProvider provider)
        {
        }

        public FileStorage GetFileStorage(Guid id)
        {
            throw new NotImplementedException();
        }

        public FileVersionStorage GetFileVerionStorage(Guid id)
        {
            throw new NotImplementedException();
        }
    }

    internal class StorageIndexCache
    {
        private IndexWriter writer;
        private IndexReader reader;
    }

    public interface IndexWriter
    {
    }

    public interface IndexReader
    {
    }

    public class FileIndexWriter : IndexWriter
    {
        private System.IO.BinaryWriter writer;

        public FileIndexWriter(System.IO.Stream stream)
        {
            if (!stream.CanWrite) throw new System.Exception();
            if (!stream.CanSeek) throw new System.Exception();
            writer = new System.IO.BinaryWriter(stream);
        }

        public void Writer(StorageInfo index)
        {
            if (index == null) throw new Exception();
            if (string.IsNullOrEmpty(index.Name)) throw new Exception();
            writer.Write(index.ID.ToByteArray());
            writer.Write(index.Created.ToOADate());
            var name = index.Name ?? string.Empty;
            var namebytes = System.Text.Encoding.UTF8.GetBytes(name);
            var span = StorageInfo.NameSizeBuffer - namebytes.Length;

            if (span > 0)
            {
                writer.Write(namebytes.Length);
                writer.Write(namebytes);
            }
            else
            {
                writer.Write(StorageInfo.NameSizeBuffer);
                writer.Write(namebytes, 0, StorageInfo.NameSizeBuffer);
            }
        }
    }

    public class FileIndexReader : IndexReader
    {
        private System.IO.BinaryReader reader;

        public FileIndexReader(System.IO.Stream stream)
        {
            if (!stream.CanRead) throw new System.Exception();
            if (!stream.CanSeek) throw new System.Exception();
            if (stream.Position != 0) stream.Seek(0, System.IO.SeekOrigin.Begin);
            reader = new System.IO.BinaryReader(stream);
        }

        public bool IsEnd
        {
            get { return reader.BaseStream.Position == reader.BaseStream.Length; }
        }

        public StorageInfo Read()
        {
            if (IsEnd) return null;
            var guidbytes = reader.ReadBytes(16);
            if (guidbytes.Length == 0) return null;
            var oaDate = reader.ReadDouble();
            var length = reader.ReadInt32();
            var namebytes = reader.ReadBytes(length);

            return new FileStorageInfo() { ID = new Guid(guidbytes), Created = DateTime.FromOADate(oaDate), Name = System.Text.Encoding.UTF8.GetString(namebytes).Replace("\0", string.Empty).Trim() };
        }

        public IEnumerable<Guid> ReadAllID()
        {
            while (!IsEnd)
            {
                var guidbytes = reader.ReadBytes(16);
                if (guidbytes.Length == 0) break;

                var id = new Guid(guidbytes);
                //  reader.ReadDouble();
                reader.BaseStream.Seek(8, System.IO.SeekOrigin.Current);
                var length = reader.ReadInt32();
                reader.BaseStream.Seek(length, System.IO.SeekOrigin.Current);
                yield return id;
            }
        }

        public IEnumerable<StorageInfo> ReadToEnd()
        {
            StorageInfo index = Read();

            while (index != null)
            {
                yield return index;

                index = Read();
            }
        }
    }

    public interface IFileVersionStorageProvider : IFileStorageProvider
    {
        int[] GetVersions();

        Stream Get(Guid id, int version);
    }

    public interface IFileStorageProvider
    {
        bool CanDelete { get; }
        bool CanUpdate { get; }

        void Delete(Guid id);

        Stream Get(Guid id);

        FileStorageInfo GetIndex(Guid id);

        void Update(Guid id, Stream stream);
    }

    public class Physical64MStorageProvider : IFileStorageProvider
    {
        public Physical64MStorageProvider()
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

namespace Library.Storage.Video
{
    public interface IVideoStorage : IFileStorage, Image.IImageThumbnailStorage
    {
        void AddVideo(Stream stream);
    }
}

namespace Library.Storage.Image
{
    public interface IImageThumbnailStorage
    {
        bool HasThumbnail { get; }

        void AddThumbnail(Stream stream);

        Stream GetThumbnail();
    }

    public interface IImageStorage : IFileStorage, IImageThumbnailStorage
    {
        int[] Diagonals { get; }

        void Add(Stream stream, int level);

        Stream Get(int level);
    }

    public interface IImageStorageProvider : IFileStorageProvider
    {
        Stream Get(Guid ID, int level);

        bool Exist(Guid ID, int level);

        void Add(Guid ID, Stream stream, int level);

        byte[] GetRange(Guid iD, int index, int size);
    }

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
            throw new NotImplementedException();
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

    public static class IamgeType
    {
        public const int Thumbnail = 0;
    }

    public class ImageStorage : FileStorage, IImageStorage
    {
        public ImageStorage(IImageStorageProvider provider, Guid id) : base(provider, id)
        {
            _provider = provider;
        }

        private IImageStorageProvider _provider;
        public int[] Diagonals { get; private set; }

        public bool HasThumbnail
        {
            get
            {
                return _provider.Exist(ID, 0);
            }
        }

        public Stream Get(int diagonal)
        {
            return _provider.Get(ID, diagonal);
        }

        public Stream GetThumbnail()
        {
            return _provider.Get(ID, IamgeType.Thumbnail);
        }

        public void Add(Stream stream, int diagonal)
        {
            _provider.Add(ID, stream, diagonal);
        }

        public void AddThumbnail(Stream stream)
        {
            _provider.Add(ID, stream, IamgeType.Thumbnail);
        }

        public Stream Get()
        {
            return _provider.Get(ID, Diagonals == null ? 0 : Diagonals.Max());
        }

        public byte[] GetRange(int index, int size)
        {
            return _provider.GetRange(ID, index, size);
        }
    }
}