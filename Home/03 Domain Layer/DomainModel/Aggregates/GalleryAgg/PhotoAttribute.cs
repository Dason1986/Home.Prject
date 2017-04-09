using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    [System.ComponentModel.Description("相片屬性"),
        System.ComponentModel.DisplayName("相片屬性")]
    public class PhotoAttribute : Home.DomainModel.Aggregates.Attribute
    {
        public PhotoAttribute()
        {

        }
        public PhotoAttribute(ICreatedInfo createinfo) : base(createinfo)
        {

        }


        public virtual Photo Owner { get; set; }
        
    }
}