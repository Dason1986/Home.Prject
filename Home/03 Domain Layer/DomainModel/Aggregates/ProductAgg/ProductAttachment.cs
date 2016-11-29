using Home.DomainModel.Aggregates.FileAgg;
using System;

namespace Home.DomainModel.Aggregates.ProductAgg
{
    public class ProductAttachment : Attachment
    {
        public Guid ProductID { get; set; }

       public virtual  ProductItem Product { get; set; }
    }
}