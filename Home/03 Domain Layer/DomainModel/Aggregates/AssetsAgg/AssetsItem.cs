using Home.DomainModel.Aggregates.ProductAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;

namespace Home.DomainModel.Aggregates.AssetsAgg
{

    [System.ComponentModel.Description("資產項目"),
        System.ComponentModel.DisplayName("資產項目")]
    public class AssetsItem : AuditedEntity
    {
        public AssetsItem()
        {

        }
        public AssetsItem(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [System.ComponentModel.Description("序號"),
      System.ComponentModel.DisplayName("序號")]
        public string SnCode { get; set; }

        [System.ComponentModel.Description("名稱"),
      System.ComponentModel.DisplayName("名稱")]
        public string Name { get; set; }

        [System.ComponentModel.Description("公用"),
      System.ComponentModel.DisplayName("公用")]
        public bool IsPublic { get; set; }

        [System.ComponentModel.Description("擁有人"),
      System.ComponentModel.DisplayName("擁有人")]
        public Guid ContactID { get; set; }
        public virtual ContactAgg.ContactProfile Contact { get; set; }

        [System.ComponentModel.Description("購買訂單編號"),
      System.ComponentModel.DisplayName("購買訂單編號")]
        public Guid OrderID { get; set; }

        public virtual PurchaseOrder Order { get; set; }
        [System.ComponentModel.Description("產品編號"),
System.ComponentModel.DisplayName("產品編號")]
        public Guid ProductID { get; set; }
        [System.ComponentModel.Description("購買訂單"),
         System.ComponentModel.DisplayName("購買訂單")]
        public virtual ProductItem Product { get; set; }

        [System.ComponentModel.Description("是否損壞"),
System.ComponentModel.DisplayName("是否損壞")]
        public bool IsBroken { get; set; }

        [System.ComponentModel.Description("損壞日期"),
System.ComponentModel.DisplayName("損壞日期")]
        public DateTime? BrokenDate { get; set; }
    }
}