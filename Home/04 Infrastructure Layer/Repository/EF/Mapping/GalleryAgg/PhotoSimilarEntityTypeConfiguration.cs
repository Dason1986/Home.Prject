using Home.DomainModel.Aggregates.GalleryAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.GalleryAgg
{


    internal class PhotoSimilarEntityTypeConfiguration : EntityTypeConfiguration<PhotoSimilar>
    {
        public PhotoSimilarEntityTypeConfiguration()
        {
            this.HasRequired(t => t.LeftOwner).WithMany()
      .HasForeignKey(t => t.LeftPhotoID).WillCascadeOnDelete(false);

            this.HasRequired(t => t.RightOwner).WithMany()
   .HasForeignKey(t => t.RightPhotoID).WillCascadeOnDelete(false);

            ToTable("PhotoSimilar");
        }
    }
}