using System;
using System.IO;

namespace Library.Storage
{
    /// <summary>
    ///
    /// </summary>
    public interface IFileStorage: IDisposable
    {
        /// <summary>
        ///
        /// </summary>
        Guid ID { get; }

        bool Exists { get; }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Stream Get();

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        byte[] GetRange(int index, int size);

        void Delete();
    }
}