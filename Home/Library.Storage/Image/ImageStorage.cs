using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Storage.Image
{
    public class ImageStorage : FileStorage, IImageStorage
    {
        public ImageStorage(IImageStorageProvider provider, Guid id) : base(provider, id)
        {
            _provider = provider;
            Diagonals = new int[]{ };
        }

        public ImageStorage(IImageStorageProvider provider, Guid id, int[] diagonals) : base(provider, id)
        {
            _provider = provider;
            Diagonals = diagonals;
        }

        private readonly IImageStorageProvider _provider;
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
            Diagonals = new List<int>(Diagonals) { diagonal }.Distinct().ToArray();
            _provider.Add(ID, stream, diagonal);
        }

        public void AddThumbnail(Stream stream)
        {
            _provider.Add(ID, stream, IamgeType.Thumbnail);
        }

        public override Stream Get()
        {
            return _provider.Get(ID, Diagonals == null ? 0 : Diagonals.Max());
        }

        public override byte[] GetRange(int index, int size)
        {
            return _provider.GetRange(ID, index, size);
        }
    }
}