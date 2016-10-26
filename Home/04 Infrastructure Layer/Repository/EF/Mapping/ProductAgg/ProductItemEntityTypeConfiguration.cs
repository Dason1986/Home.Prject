using DomainModel.Aggregates.ProductAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.ProductAgg
{
    internal class ProductItemEntityTypeConfiguration : EntityTypeConfiguration<ProductItem>
    {
        public ProductItemEntityTypeConfiguration()
        {

            HasMany(n => n.Attachments).WithRequired()
.HasForeignKey(c => c.ProductID)
.WillCascadeOnDelete(false);
            ToTable("ProductItem");
        }
    }
}