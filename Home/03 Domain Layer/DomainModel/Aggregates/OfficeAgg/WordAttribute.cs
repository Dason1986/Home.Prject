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
   
}