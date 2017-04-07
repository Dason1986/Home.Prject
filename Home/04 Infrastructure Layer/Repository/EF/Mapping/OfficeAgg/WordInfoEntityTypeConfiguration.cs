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
            HasMany(c => c.Elements)
.WithRequired()
.HasForeignKey(c => c.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("WordInfo");
        }
    }
    internal class WordObjectElementEntityTypeConfiguration : EntityTypeConfiguration<WordObjectElement>
    {
        public WordObjectElementEntityTypeConfiguration()
        {

            this.HasRequired(t => t.Owner)
.WithMany()
.HasForeignKey(t => t.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("WordObjectElement");
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

    internal class PDFInfoEntityTypeConfiguration : EntityTypeConfiguration<PDFInfo>
    {
        public PDFInfoEntityTypeConfiguration()
        {

            HasMany(c => c.Attributes)
.WithRequired()
.HasForeignKey(c => c.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("PDFInfo");
        }
    }
    internal class PDFAttributeEntityTypeConfiguration : EntityTypeConfiguration<PDFAttribute>
    {
        public PDFAttributeEntityTypeConfiguration()
        {

            this.HasRequired(t => t.Owner)
.WithMany()
.HasForeignKey(t => t.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("PDFAttribute");
        }
    }
}