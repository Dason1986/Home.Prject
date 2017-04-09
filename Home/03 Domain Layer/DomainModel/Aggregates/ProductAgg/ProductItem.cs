using Home.DomainModel.Aggregates.AssetsAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.ProductAgg
{
    [System.ComponentModel.Description("產品"),
           System.ComponentModel.DisplayName("產品")]
    public class ProductItem : AuditedEntity
    {

        public ProductItem()
        {

        }

        public ProductItem(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(50)]
        [System.ComponentModel.Description("名稱"),
          System.ComponentModel.DisplayName("名稱")]
        public string Name { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("型號"),
          System.ComponentModel.DisplayName("型號")]
        public string Modle { get; set; }

        [System.ComponentModel.Description("價錢"),
          System.ComponentModel.DisplayName("價錢")]
        public Money Value { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("條形碼"),
          System.ComponentModel.DisplayName("條形碼")]
        public string BarCode { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("標籤"),
          System.ComponentModel.DisplayName("標籤")]
        public string Tag { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("所屬公司"),
          System.ComponentModel.DisplayName("所屬公司")]
        public string Company { get; set; }

        public IList<ProductAttachment> Attachments { get; set; }
    }
}
