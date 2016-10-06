using DomainModel.Aggregates.GalleryAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.GalleryAgg
{
    internal class PhotoEntityTypeConfiguration : EntityTypeConfiguration<Photo>
    {
        public PhotoEntityTypeConfiguration()
        {
            this.HasRequired(t => t.ParentAlbum)
      .WithMany()
      .HasForeignKey(t => t.AlbumID)
      .WillCascadeOnDelete(false);
            this.HasRequired(t => t.File)
   .WithMany()
   .HasForeignKey(t => t.FileID)
   .WillCascadeOnDelete(false);

            HasMany(c => c.Attributes)
.WithRequired()
.HasForeignKey(c => c.PhotoID)
.WillCascadeOnDelete(false);
            ToTable("Photo");
        }
    }


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