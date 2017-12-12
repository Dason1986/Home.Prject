using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;

namespace Home.DomainModel.Aggregates.OfficeAgg
{
    [System.ComponentModel.Description("Word文檔"),
           System.ComponentModel.DisplayName("Word文檔")]
    public class WordInfo : AuditedEntity
    {
        public WordInfo(ICreatedInfo createinfo) : base(createinfo) { Summary = new OfficeInfo(); }
        public WordInfo()
        {

        }

        public virtual ICollection<WordAttribute> Attributes { get; set; }
        public virtual OfficeInfo Summary { get; set; }
        public OffileFileType OffileFileType { get; set; }


        [System.ComponentModel.Description("文件ID"),
         System.ComponentModel.DisplayName("文件ID")]
        public Guid FileID { get; set; }
        public virtual DomainModel.Aggregates.FileAgg.FileInfo File { get; set; }
    }

}
