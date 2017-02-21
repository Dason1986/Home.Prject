using System;

namespace Library.Storage
{

    /// <summary>
    ///
    /// </summary>
    public abstract class StorageInfo
    {
        /// <summary>
        ///
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        internal const int NameSizeBuffer = 100;
    }
}