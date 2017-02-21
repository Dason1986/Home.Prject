using System;

namespace Library.Storage
{

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
}