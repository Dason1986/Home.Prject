using System.IO;

namespace Library.Storage.Image
{
    public interface IImageThumbnailStorage
    {
        bool HasThumbnail { get; }

        void AddThumbnail(Stream stream);

        Stream GetThumbnail();
    }
}