using Home.DomainModel.Aggregates.FileAgg;
using Library.ComponentModel.Model;
using System;

namespace Home.DomainModel.Aggregates.ProductAgg
{
    [System.ComponentModel.Description("產品附件"),
           System.ComponentModel.DisplayName("產品附件")]
    public class ProductAttachment : Attachment
    {
        public ProductAttachment()
        {

        }
        public ProductAttachment(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("產品ID"),
       System.ComponentModel.DisplayName("產品ID")]
        public Guid ProductID { get; set; }

        public virtual ProductItem Product { get; set; }
    }
}