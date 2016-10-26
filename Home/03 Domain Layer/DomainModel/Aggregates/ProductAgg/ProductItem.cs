using Library.Domain;
using System.Collections.Generic;

namespace DomainModel.Aggregates.ProductAgg
{
    public class ProductItem : AuditedEntity
    {
        public string Name { get; set; }

        public string Modle { get; set; }

        public decimal Value { get; set; }

        public string BarCode { get; set; }

        public string Tag { get; set; }

        public string Company { get; set; }

        public IList<ProductAttachment> Attachments { get; set; }
    }
}
