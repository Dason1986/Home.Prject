using DomainModel.Aggregates.AssetsAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.AssetsAgg
{
    internal class PurchaseLineItemEntityTypeConfiguration : EntityTypeConfiguration<PurchaseLineItem>
    {
        public PurchaseLineItemEntityTypeConfiguration()
        {
            HasRequired(c => c.Order).WithMany().HasForeignKey(n => n.OrderID);
            HasRequired(c => c.Product).WithMany().HasForeignKey(n => n.ProductID);

            ToTable("PurchaseLineItem");
        }
    }
}