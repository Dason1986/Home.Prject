using DomainModel.Aggregates.AssetsAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.AssetsAgg
{

    internal class PurchaseOrderEntityTypeConfiguration : EntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderEntityTypeConfiguration()
        {
            HasRequired(c => c.OrderUser).WithMany().HasForeignKey(n => n.OrderUserID);
            this.HasMany(c => c.Items)
                   .WithRequired()
                   .HasForeignKey(c => c.OrderID)
                   .WillCascadeOnDelete(false);
            ToTable("PurchaseOrder");
        }
    }
}