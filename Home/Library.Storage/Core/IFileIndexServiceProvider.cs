using System.Linq;

namespace Library.Storage
{
    /// <summary>
    ///
    /// </summary>
    public interface IFileIndexServiceProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IQueryable<FileStorageInfo> CreateSet();
    }
}