using Library.Domain;
using System.Collections.Generic;

namespace Home.DomainModel.Aggregates.OfficeAgg
{
    [System.ComponentModel.Description("Word文檔"),
           System.ComponentModel.DisplayName("Word文檔")]
    public class WordInfo : AuditedEntity
    {

        [System.ComponentModel.Description("頁數"),
        System.ComponentModel.DisplayName("頁數")]
        public int PagesCount { get; set; }
        [System.ComponentModel.Description("總字符數"),
        System.ComponentModel.DisplayName("總字符數")]
        public int CharCount { get; set; }
        [System.ComponentModel.Description("總中文數"),
        System.ComponentModel.DisplayName("總中文數")]
        public int ChineseCount { get; set; }
        [System.ComponentModel.Description("圖像數"),
        System.ComponentModel.DisplayName("圖像數")]
        public int ImageCount { get; set; }
        [System.ComponentModel.Description("超鏈接數"),
        System.ComponentModel.DisplayName("超鏈接數")]
        public int LinkCount { get; set; }

        public virtual ICollection<WordAttribute> Attributes { get; set; }

        public virtual ICollection<WordObjectElement> Elements { get; set; }
    }
    [System.ComponentModel.Description("PDF文檔"),
       System.ComponentModel.DisplayName("PDF文檔")]
    public class PDFInfo : AuditedEntity
    {


        [System.ComponentModel.Description("頁數"),
    System.ComponentModel.DisplayName("頁數")]
        public int PagesCount { get; set; }
        [System.ComponentModel.Description("總字符數"),
        System.ComponentModel.DisplayName("總字符數")]
        public int CharCount { get; set; }
        [System.ComponentModel.Description("總中文數"),
        System.ComponentModel.DisplayName("總中文數")]
        public int ChineseCount { get; set; }
        [System.ComponentModel.Description("圖像數"),
        System.ComponentModel.DisplayName("圖像數")]
        public int ImageCount { get; set; }
        [System.ComponentModel.Description("超鏈接數"),
        System.ComponentModel.DisplayName("超鏈接數")]
        public int LinkCount { get; set; }

        public virtual ICollection<PDFAttribute> Attributes { get; set; }
    }
}
