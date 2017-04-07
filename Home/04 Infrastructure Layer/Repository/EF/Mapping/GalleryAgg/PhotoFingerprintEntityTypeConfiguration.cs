using Home.DomainModel.Aggregates.GalleryAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.GalleryAgg
{

    internal class PhotoFingerprintEntityTypeConfiguration : EntityTypeConfiguration<PhotoFingerprint>
    {
        public PhotoFingerprintEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Owner).WithMany()
      .HasForeignKey(t => t.PhotoID).WillCascadeOnDelete(false);

            

            ToTable("PhotoFingerprint");
        }
    }
}