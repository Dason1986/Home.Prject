using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;

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
}