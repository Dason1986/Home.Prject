using Home.DomainModel.Aggregates.GalleryAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{
    public class FileInfoExtend : CreateEntity
    {
        public FileInfoExtend()
        {
        }

        public FileInfoExtend(ICreatedInfo createinfo) : base(createinfo)
        {
        }

        public string Comments { get; set; }
        public string KeyWords { get; set; }
        public Guid FileID { get; set; }

        public byte[] BarCodeBuffer { get; set; }
        public byte[] QRCodeBuffer { get; set; }
        public string BarCode { get; set; }
        public string QRCode { get; set; }
        public string Sequence { get; set; }

        public virtual ICollection<FileAttribute> Attributes { get; set; }
    }

    public class FileAttribute : AuditedEntity
    {
        public FileAttribute()
        {
        }

        public FileAttribute(ICreatedInfo createinfo) : base(createinfo)
        {
        }

        public Guid OwnerID { get; set; }

        public virtual FileInfoExtend Owner { get; set; }

        [StringLength(50)]
        public string AttKey { get; set; }

        [StringLength(255)]
        public string AttValue { get; set; }
    }
}