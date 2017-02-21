using System;

namespace Library.Storage
{

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
}