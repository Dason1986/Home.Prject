using DomainModel.Aggregates.AssetsAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.AssetsAgg
{
    internal class AssetsItemEntityTypeConfiguration : EntityTypeConfiguration<AssetsItem>
    {
        public AssetsItemEntityTypeConfiguration()
        {
            HasRequired(c => c.Order).WithMany().HasForeignKey(n => n.OrderID);
            HasRequired(c => c.Product).WithMany().HasForeignKey(n => n.ProductID);
            HasRequired(c => c.Contact).WithMany().HasForeignKey(n => n.ContactID);

            ToTable("AssetsItem");
        }
    }
}