using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;

namespace Home.DomainModel.Aggregates.FileAgg
{
    [System.ComponentModel.Description("文件擴展信息"),
           System.ComponentModel.DisplayName("文件擴展信息")]
    public class FileInfoExtend : Entity
    {
        public FileInfoExtend()
        {
        }

        public FileInfoExtend(ICreatedInfo createinfo) : base(createinfo)
        {
        }
        [System.ComponentModel.Description("說明"),
        System.ComponentModel.DisplayName("說明")]
        public string Comments { get; set; }
        [System.ComponentModel.Description("關係詞"),
System.ComponentModel.DisplayName("關係詞")]
        public string KeyWords { get; set; }
        [System.ComponentModel.Description("文件編號，外鍵"),
System.ComponentModel.DisplayName("文件編號")]
        public Guid FileID { get; set; }
        [System.ComponentModel.Description("條形碼圖形"),
        System.ComponentModel.DisplayName("條形碼圖形")]
        public byte[] BarCodeBuffer { get; set; }
        [System.ComponentModel.Description("二維碼圖形"),
System.ComponentModel.DisplayName("二維碼圖形")]
        public byte[] QRCodeBuffer { get; set; }
        [System.ComponentModel.Description("條形碼"),
System.ComponentModel.DisplayName("條形碼")]
        public string BarCode { get; set; }
        [System.ComponentModel.Description("二維碼內容"),
System.ComponentModel.DisplayName("二維碼內容")]
        public string QRCode { get; set; }
        [System.ComponentModel.Description("文件顯示編號"),
System.ComponentModel.DisplayName("文件顯示編號")]
        public string Sequence { get; set; }
        [System.ComponentModel.Description("文件屬性"),
        System.ComponentModel.DisplayName("文件屬性")]
        public virtual ICollection<FileAttribute> Attributes { get; set; }
    }
}