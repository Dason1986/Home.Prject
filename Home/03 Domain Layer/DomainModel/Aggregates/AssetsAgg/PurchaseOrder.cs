using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;

namespace Home.DomainModel.Aggregates.AssetsAgg
{
    public  class PurchaseOrder: CreateEntity
    {
        public PurchaseOrder()
        {

        }
        public PurchaseOrder(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public IList<PurchaseLineItem> Items { get; set; }

        public PayTpye PayType { get; set; }

        public Money PayAmout { get; set; }

        public Guid OrderUserID { get; set; }



        public virtual UserAgg.UserProfile OrderUser { get; set; }
    }
}
