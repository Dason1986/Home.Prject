using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{
    [System.ComponentModel.Description("文件屬性"),
        System.ComponentModel.DisplayName("文件屬性")]
    public class FileAttribute : Attribute
    {
        public FileAttribute()
        {
        }

        public FileAttribute(ICreatedInfo createinfo) : base(createinfo)
        {
        }

      

        public virtual FileInfoExtend Owner { get; set; }

       
    }
}