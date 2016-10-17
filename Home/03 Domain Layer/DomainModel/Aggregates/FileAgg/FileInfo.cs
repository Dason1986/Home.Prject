using DomainModel.Aggregates.GalleryAgg;
using Library.ComponentModel.Model;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.Aggregates.FileAgg
{
    public class FileInfo : Entity
    {
        public FileInfo()
        {

        }
        public FileInfo(ICreatedInfo createinfo) : base(createinfo)
        {

        }

        public string FileName { get; set; }

        public string FullPath { get; set; }

        public long FileSize { get; set; }
        [StringLength(32)]
        public string MD5 { get; set; }
        [StringLength(50)]
        public string Extension { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
