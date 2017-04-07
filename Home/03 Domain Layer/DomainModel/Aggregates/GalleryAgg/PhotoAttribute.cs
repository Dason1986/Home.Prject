using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.GalleryAgg
{

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