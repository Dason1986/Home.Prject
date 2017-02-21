using System.Linq;

namespace Library.Storage
{
    /// <summary>
    ///
    /// </summary>
    public interface IIndexEngineProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IQueryable<FileStorageInfo> CreateSet();
    }
}