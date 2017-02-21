using System;

namespace Library.Storage
{

    public class FileStorageInfo : StorageInfo
    {
        public DateTime Modified { get; set; }

        public static FileStorageInfo Create(string name)
        {
            return new FileStorageInfo { Name = name, Created = DateTime.Now, ID = Guid.NewGuid() };
        }
    }
}