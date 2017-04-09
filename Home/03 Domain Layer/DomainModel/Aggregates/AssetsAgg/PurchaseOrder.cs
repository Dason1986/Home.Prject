using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;

namespace Home.DomainModel.Aggregates.AssetsAgg
{
    [System.ComponentModel.Description("購買訂單"),
          System.ComponentModel.DisplayName("購買訂單")]
    public  class PurchaseOrder: CreateEntity
    {
        public PurchaseOrder()
        {

        }
        public PurchaseOrder(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("購買清单"),
          System.ComponentModel.DisplayName("購買清单")]
        public IList<PurchaseLineItem> Items { get; set; }

        [System.ComponentModel.Description("支付方式"),
          System.ComponentModel.DisplayName("支付方式")]
        public PayTpye PayType { get; set; }

        [System.ComponentModel.Description("支付金额"),
          System.ComponentModel.DisplayName("支付金额")]
        public Money PayAmout { get; set; }

        [System.ComponentModel.Description("购买人编号"),
          System.ComponentModel.DisplayName("购买人编号")]
        public Guid OrderUserID { get; set; }



        public virtual UserAgg.UserProfile OrderUser { get; set; }
    }
}
