using System;
using System.IO;
using System.Linq;

namespace Library.Storage
{
    /// <summary>
    ///
    /// </summary>
    public abstract class StorageStoryContext
    {
        protected StorageStoryContext(IIndexEngineProvider indexProvider, IFileEngineProvider provider)
        {
            _indexProvider = indexProvider;
            _provider = provider;
        }

        private readonly IIndexEngineProvider _indexProvider;
        private readonly IFileEngineProvider _provider;

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
            FileStorage storage = _provider.Get(fileinfo.ID);
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
            FileStorage storage = _provider.Get(fileinfo.ID);
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
}