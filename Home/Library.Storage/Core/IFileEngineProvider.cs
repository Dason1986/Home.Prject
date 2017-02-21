using System;
using Library.Storage.Image;
using Library.Storage.Video;

namespace Library.Storage
{
    /// <summary>
    ///
    /// </summary>
    public interface IFileEngineProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FileStorage Get(Guid id);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IFileVersionStorageProvider CreateVersionStorageProvider();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IVideoStorage CreateVideoStorage();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IImageStorageProvider CreateImageStorageProvider();
    }
}