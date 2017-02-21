using System;
using System.IO;

namespace Library.Storage
{

    public class FileVersionStorage : FileStorage
    {
        public FileVersionStorage(IFileVersionStorageProvider provider, Guid fileid) : base(provider, fileid)
        {
            VersionProvider = provider;
        }

        protected internal IFileVersionStorageProvider VersionProvider { get; set; }

        public virtual int[] GetVersions()
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
}