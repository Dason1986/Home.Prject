using Library.ComponentModel.Model;
using System;

namespace DomainModel.Aggregates.GalleryAgg
{

    public class PhotoFingerprint : Entity
    {
        public PhotoFingerprint()
        {

        }
        public PhotoFingerprint(ICreatedInfo createinfo) : base(createinfo)
        {

        }

        public Guid PhotoID { get; set; }

        public virtual Photo Owner { get; set; }

        public byte[] Fingerprint { get; set; }
        public SimilarAlgorithm Algorithm { get; set; }
    }
}