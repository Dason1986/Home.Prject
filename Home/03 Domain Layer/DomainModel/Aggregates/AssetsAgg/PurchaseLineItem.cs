using Home.DomainModel.Aggregates.ProductAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.AssetsAgg
{
    public class PurchaseLineItem: CreateEntity
    {
        public PurchaseLineItem()
        {

        }
        public PurchaseLineItem(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public Guid ProductID { get; set; }

        public virtual ProductItem Product { get; set; }

        public decimal Quantity { get; set; }

        public Money Price { get; set; }

        public Guid OrderID { get; set; }

        public virtual PurchaseOrder Order { get; set; }
    }

    public class AssetsItem:AuditedEntity
    {
        public AssetsItem()
        {

        }
        public AssetsItem(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public string SnCode { get; set; }
        
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public Guid ContactID { get; set; }
        public virtual ContactAgg.ContactProfile Contact { get; set; }

        public Guid OrderID { get; set; }

        public virtual PurchaseOrder Order { get; set; }
        public Guid ProductID { get; set; }

        public virtual ProductItem Product { get; set; }

        public bool IsBroken { get; set; }

        public DateTime BrokenDate { get; set; }
    }
}
