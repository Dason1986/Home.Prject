using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.AssetsAgg
{
    public  class PurchaseOrder: CreateEntity
    {
        public IList<PurchaseLineItem> Items { get; set; }

        public PayTpye PayType { get; set; }

        public Money PayAmout { get; set; }

        public Guid OrderUserID { get; set; }



        public virtual UserAgg.UserProfile OrderUser { get; set; }
    }
}
