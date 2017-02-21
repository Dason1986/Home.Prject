using System;
using System.Runtime.Serialization;

namespace Library.Storage
{

    [Serializable]
    public class FileStorageNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public FileStorageNotFoundException()
        {
        }

        public FileStorageNotFoundException(string message) : base(message)
        {
        }

        public FileStorageNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FileStorageNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}