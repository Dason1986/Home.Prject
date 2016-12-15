using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.IO.Storage
{
    public abstract class StorageIndex
    {
        public Guid ID { get; set; }
        public DateTime Created { get; set; }

        public string Name { get; set; }

        internal const int NameSizeBuffer = 100;
    }
    public class FileStorage
    {
        protected internal FileStorageIndex Index { get; set; }
        protected internal IFileStorageProvider Provider { get; set; }
        public FileStorage(IFileStorageProvider provider)
        {
            Provider = provider;
        }
        public Stream Get()
        {
            return Provider.Get(Index.ID);
        }

        public virtual void Update(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }


    public class FileVersionStorage : FileStorage
    {
        public FileVersionStorage(IFileStorageProvider provider) : base(provider)
        {
        }

        public Stream Get(int version)
        {

            return Provider.Get(Index.ID, version);
        }

        public override void Update(Stream stream)
        {
            //            Index.Versoin;
            throw new NotImplementedException();
        }
    }
    public class FileStorageIndex : StorageIndex
    {
        public DateTime Modified { get; set; }
        public static FileStorageIndex Create(string name)
        {
            return new FileStorageIndex { Name = name, Created = DateTime.Now, ID = Guid.NewGuid() };
        }
    }
    public class DirectoryStorageIndex : StorageIndex
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

    public interface ISingleLayerFileStory
    {
        FileStorage GetFileStorage(Guid id);
        FileStorage GetFileVerionStorage(Guid id);
    }
    public class SingleLayerFileStory : ISingleLayerFileStory
    {

        public SingleLayerFileStory(IFileStorageProvider provider)
        {

        }
        public FileStorage GetFileStorage(Guid id)
        {
            throw new NotImplementedException();
        }

        public FileStorage GetFileVerionStorage(Guid id)
        {
            throw new NotImplementedException();
        }
    }

    class StorageIndexCache
    {

    }

    public class FileIndexWriter
    {
        System.IO.BinaryWriter writer;
        public FileIndexWriter(System.IO.Stream stream)
        {
            if (!stream.CanWrite) throw new System.Exception();
            if (!stream.CanSeek) throw new System.Exception();
            writer = new System.IO.BinaryWriter(stream);

        }

        public void Writer(StorageIndex index)
        {
            if (index == null) throw new Exception();
            if (string.IsNullOrEmpty(index.Name)) throw new Exception();
            writer.Write(index.ID.ToByteArray());
            writer.Write(index.Created.ToOADate());
            var name = index.Name ?? string.Empty;
            var namebytes = System.Text.Encoding.UTF8.GetBytes(name);
            var span = StorageIndex.NameSizeBuffer - namebytes.Length;

            if (span > 0)
            {
                writer.Write(namebytes.Length);
                writer.Write(namebytes);

            }
            else
            {
                writer.Write(StorageIndex.NameSizeBuffer);
                writer.Write(namebytes, 0, StorageIndex.NameSizeBuffer);
            }
        }

    }

    public interface IFileStorageProvider
    {
        Stream Get(Guid id);
        Stream Get(Guid id, int version);
    }
    public class FileIndexReader
    {
        System.IO.BinaryReader reader;
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
        public StorageIndex Read()
        {
            if (IsEnd) return null;
            var guidbytes = reader.ReadBytes(16);
            if (guidbytes.Length == 0) return null;
            var oaDate = reader.ReadDouble();
            var length = reader.ReadInt32();
            var namebytes = reader.ReadBytes(length);

            return new FileStorageIndex() { ID = new Guid(guidbytes), Created = DateTime.FromOADate(oaDate), Name = System.Text.Encoding.UTF8.GetString(namebytes).Replace("\0", string.Empty).Trim() };

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
        public IEnumerable<StorageIndex> ReadToEnd()
        {


            StorageIndex index = Read();

            while (index != null)
            {
                yield return index;

                index = Read();
            }

        }
    }
}


namespace Library.IO.Storage.Image
{


    public interface IImageStorage
    {
        Guid ID { get; }

        int[] Diagonals { get; }
        bool HasThumbnail { get; }

        void Update(Stream stream, int level);
        Stream Get(int level);
        Stream GetThumbnail();
        void UpdateThumbnail(Stream stream);

        void Delete();
    }
    public interface IImageStorageProvider
    {
        Stream Get(int level);
        bool Exist(int level);
        void Update(Stream stream, int level);
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
        string rawFormat = ".jpeg";
        string _path;
        void setPath(string path)
        {
            if (_path == path) return;
            if (string.Equals(_path, path, StringComparison.OrdinalIgnoreCase)) return;
            _path = path;
        }
        string GetfilePath(int level)
        {
            return Path.Combine(_path, level.ToString() + rawFormat);

        }
        public Stream Get(int level)
        {
            var file = GetfilePath(level);
            if (File.Exists(file))
            {
                return File.Open(file, FileMode.Open, FileAccess.Read);
            }
            throw new FileNotFoundException(file);
        }
        public bool Exist(int level)
        {
            return File.Exists(GetfilePath(level));
        }

        public void Update(Stream stream, int level)
        {
            var file = GetfilePath(level);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            stream.Seek(0, SeekOrigin.Begin);
            FileStream writer = new FileStream(file, FileMode.CreateNew, FileAccess.ReadWrite);
            stream.CopyTo(writer);
            writer.Close();
            writer.Dispose();
        }
    }
    public static class IamgeType
    {
        public const int Thumbnail = 0;
    }
    public class ImageStorage : IImageStorage
    {

        public ImageStorage(Guid id, IImageStorageProvider provider)
        {
            _provider = provider;
            ID = id;
        }
        IImageStorageProvider _provider;
        public int[] Diagonals { get; private set; }

        public Guid ID { get; private set; }

        public bool HasThumbnail
        {
            get
            {
                return _provider.Exist(0);
            }
        }

        public Stream Get(int diagonal)
        {
            return _provider.Get(diagonal);
        }

        public Stream GetThumbnail()
        {
            return _provider.Get((int)IamgeType.Thumbnail);
        }
        public void UpdateThumbnail(Stream stream)
        {
            _provider.Update(stream, IamgeType.Thumbnail);
        }
        public void Update(Stream stream, int diagonal)
        {
            _provider.Update(stream, diagonal);
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}

