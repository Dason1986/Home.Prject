using Home.DomainModel.Aggregates.GalleryAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{
    public class FileInfo : CreateEntity
    {
        public FileInfo()
        {

        }
        public FileInfo(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(100)]
        public string FileName { get; set; }
        [StringLength(255)]
        public string FullPath { get; set; }

        public long FileSize { get; set; }
        [StringLength(32)]
        public string MD5 { get; set; }
        [StringLength(50)]
        public string Extension { get; set; }
        public SourceType SourceType { get; set; }
        public virtual FileInfoExtend Extend { get; set; }
        public virtual Photo Photo { get; set; }
    }

}
