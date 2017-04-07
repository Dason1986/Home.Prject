using Library.Domain;
using System.Collections.Generic;

namespace Home.DomainModel.Aggregates.OfficeAgg
{
    public class WordInfo : AuditedEntity
    {


        public int PagesCount { get; set; }

        public int CharCount { get; set; }

        public int ChineseCount { get; set; }

        public int ImageCount { get; set; }

        public int LinkCount { get; set; }

        public virtual ICollection<WordAttribute> Attributes { get; set; }

        public virtual ICollection<WordObjectElement> Elements { get; set; }
    }
    public class PDFInfo : AuditedEntity
    {


        public int PagesCount { get; set; }

        public int CharCount { get; set; }

        public int ChineseCount { get; set; }

        public int ImageCount { get; set; }

        public int LinkCount { get; set; }

        public virtual ICollection<PDFAttribute> Attributes { get; set; }
    }
}
