using System;
using Library;

namespace HomeApplication.DomainServices
{

    [Serializable]
    public class PhotoDomainServiceException : LogicException
    {
        public PhotoDomainServiceException() { }
        public PhotoDomainServiceException(string message) : base(message) { }
        public PhotoDomainServiceException(string message, Exception inner) : base(message, inner) { }
        protected PhotoDomainServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}