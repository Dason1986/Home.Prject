using System;

namespace Library.Storage
{

    public class SingleLayerFileStory : IStorageStory
    {
        public SingleLayerFileStory(IFileStorageProvider provider)
        {
        }

        public FileStorage GetFileStorage(Guid id)
        {
            throw new NotImplementedException();
        }

        public FileVersionStorage GetFileVerionStorage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}