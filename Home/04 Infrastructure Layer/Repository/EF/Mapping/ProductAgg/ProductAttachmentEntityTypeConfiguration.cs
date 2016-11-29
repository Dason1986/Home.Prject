using Home.DomainModel.Aggregates.ProductAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.ProductAgg
{
    internal class ProductAttachmentEntityTypeConfiguration : EntityTypeConfiguration<ProductAttachment>
    {
        public ProductAttachmentEntityTypeConfiguration()
        {
            HasRequired(c => c.Product).WithMany().HasForeignKey(n => n.ProductID);

            ToTable("ProductAttachment");
        }
    }
}