using System.IO;

namespace Library.Storage.Video
{
    public interface IVideoStorage : IFileStorage, Image.IImageThumbnailStorage
    {
        void AddVideo(Stream stream);
    }
}