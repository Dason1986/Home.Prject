using Home.DomainModel.Aggregates.ProductAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.AssetsAgg
{

    [System.ComponentModel.Description("購買明細"),
        System.ComponentModel.DisplayName("購買明細")]
    public class PurchaseLineItem : Entity
    {
        public PurchaseLineItem()
        {

        }
        public PurchaseLineItem(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("產品編號"),
            System.ComponentModel.DisplayName("產品編號")]
        public Guid ProductID { get; set; }

        public virtual ProductItem Product { get; set; }

        [System.ComponentModel.Description("產品數量"),
            System.ComponentModel.DisplayName("產品數量")]
        public decimal Quantity { get; set; }

        [System.ComponentModel.Description("產品单价"),
            System.ComponentModel.DisplayName("產品单价")]
        public Money Price { get; set; }

        [System.ComponentModel.Description("購買訂單编号"),
            System.ComponentModel.DisplayName("購買訂單编号")]
        public Guid OrderID { get; set; }

        [System.ComponentModel.Description("購買訂單"),
            System.ComponentModel.DisplayName("購買訂單")]
        public virtual PurchaseOrder Order { get; set; }
    }
}
