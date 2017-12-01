using Home.DomainModel.Aggregates.GalleryAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.GalleryAgg
{
    internal class PhotoFaceEntityTypeConfiguration : EntityTypeConfiguration<PhotoFace>
    {
        public PhotoFaceEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Photo).WithMany()
      .HasForeignKey(t => t.PhotoId).WillCascadeOnDelete(false);
            this.HasRequired(t => t.Contact).WithMany()
     .HasForeignKey(t => t.ContactId).WillCascadeOnDelete(false);
            ToTable("PhotoFace");
        }
    }
}