using DomainModel.Aggregates.GalleryAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
