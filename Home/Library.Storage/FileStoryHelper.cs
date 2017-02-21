using System;
using System.IO;

namespace Library.Storage
{

    public static class FileStoryHelper
    {
        public static void InitPath(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public static string InitPath(string galleryPath, Guid id)
        {
            var path = Path.Combine(galleryPath, id.ToString());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }
    }
}