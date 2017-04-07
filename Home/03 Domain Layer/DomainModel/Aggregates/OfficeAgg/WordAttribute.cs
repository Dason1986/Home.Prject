using Library.ComponentModel.Model;

namespace Home.DomainModel.Aggregates.OfficeAgg
{
    public class WordAttribute : Attribute
    {
        public WordAttribute()
        {

        }
        public WordAttribute(ICreatedInfo createinfo) : base(createinfo)
        {

        }

        public virtual WordInfo Owner { get; set; }
    }
    public class PDFAttribute : Attribute
    {
        public PDFAttribute()
        {

        }
        public PDFAttribute(ICreatedInfo createinfo) : base(createinfo)
        {

        }

        public virtual PDFInfo Owner { get; set; }
    }
}