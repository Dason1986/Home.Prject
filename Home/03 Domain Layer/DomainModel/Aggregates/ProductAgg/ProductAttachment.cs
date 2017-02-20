using Home.DomainModel.Aggregates.FileAgg;
using Library.ComponentModel.Model;
using System;

namespace Home.DomainModel.Aggregates.ProductAgg
{
    public class ProductAttachment : Attachment
    {
        public ProductAttachment()
        {

        }
        public ProductAttachment(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public Guid ProductID { get; set; }

        public virtual ProductItem Product { get; set; }
    }
}