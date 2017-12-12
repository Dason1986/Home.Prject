using Home.DomainModel.Aggregates.OfficeAgg;
using Home.DomainModel.Aggregates.ProductAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.OfficeAgg
{
    internal class WordInfoEntityTypeConfiguration : EntityTypeConfiguration<WordInfo>
    {
        public WordInfoEntityTypeConfiguration()
        {

            HasMany(c => c.Attributes)
.WithRequired()
.HasForeignKey(c => c.OwnerID)
.WillCascadeOnDelete(false);

            this.HasRequired(t => t.File)
       .WithMany()
       .HasForeignKey(t => t.FileID)
       .WillCascadeOnDelete(false);

            ToTable("WordInfo");
        }
    }
    
    internal class WordAttributeEntityTypeConfiguration : EntityTypeConfiguration<WordAttribute>
    {
        public WordAttributeEntityTypeConfiguration()
        {

            this.HasRequired(t => t.Owner)
.WithMany()
.HasForeignKey(t => t.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("WordAttribute");
        }
    }

   
}