using System.IO;

namespace Library.Storage.Image
{

    public interface IImageStorage : IFileStorage, IImageThumbnailStorage
    {
        int[] Diagonals { get; }

        void Add(Stream stream, int level);

        Stream Get(int level);
    }
}