using Library.ComponentModel.Model;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    [System.ComponentModel.Description("相片指紋，用於比較分類"),
        System.ComponentModel.DisplayName("相片指紋")]
    public class PhotoFingerprint : Entity
    {
        public PhotoFingerprint()
        {

        }
        public PhotoFingerprint(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("相片ID"),
        System.ComponentModel.DisplayName("相片ID")]
        public Guid PhotoID { get; set; }

        public virtual Photo Owner { get; set; }
        [System.ComponentModel.Description("指紋信息"),
        System.ComponentModel.DisplayName("指紋信息")]
        public byte[] Fingerprint { get; set; }
        [System.ComponentModel.Description("算法"),
System.ComponentModel.DisplayName("算法")]
        public SimilarAlgorithm Algorithm { get; set; }
    }
}