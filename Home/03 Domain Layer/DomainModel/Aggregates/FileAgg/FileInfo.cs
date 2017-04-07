using Home.DomainModel.Aggregates.GalleryAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{
    [DisplayName("文件信息")]
    public class FileInfo : CreateEntity
    {
        public FileInfo()
        {
        }

        public FileInfo(ICreatedInfo createinfo) : base(createinfo)
        {
        }

        [StringLength(100)]
        [DisplayName("文件名稱"),Description("上傳文件時的名稱")]
        public string FileName { get; set; }

        [StringLength(255)]
        [DisplayName("文件路徑"), Description("保存文件的路徑")]
        public string FullPath { get; set; }

        [DisplayName("文件大小"), Description("文件佔用空間的大小")]
        public long FileSize { get; set; }

        [StringLength(32)]
        [DisplayName("文件MD5值"), Description("文件MD5值，判斷文件唯一")]
        public string MD5 { get; set; }

        [StringLength(50)]
        [DisplayName("文件擴展名"), Description("文件擴展名，判斷文件的類型")]
        public string Extension { get; set; }

        [DisplayName("文件來源"), Description("文件來源，文件獲取的方式")]
        public SourceType SourceType { get; set; }

        [DisplayName("文件擴展信息"), Description("文件擴展信息，文件獲取的方式")]
        public virtual FileInfoExtend Extend { get; set; }
        public virtual Photo Photo { get; set; }

        [DisplayName("文件存儲引擎編號"), Description("文件存儲引擎編號，文件保存的地方")]
        public Guid EngineID { get; set; }
        [DisplayName("文件存儲引擎"), Description("文件存儲引擎，文件保存的地方")]
        public virtual StorageEngine Engine { get; set; }
    }
}