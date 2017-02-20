using Library.ComponentModel.Model;
using Library.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.ProductAgg
{
    public class ProductItem : AuditedEntity
    {

        public ProductItem()
        {

        }

        public ProductItem(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Modle { get; set; }

        public decimal Value { get; set; }
        [StringLength(50)]
        public string BarCode { get; set; }
        [StringLength(50)]
        public string Tag { get; set; }
        [StringLength(50)]
        public string Company { get; set; }

        public IList<ProductAttachment> Attachments { get; set; }
    }
}
