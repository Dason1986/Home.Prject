using DomainModel;
using DomainModel.Aggregates.FileAgg;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.ContactAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.GalleryAgg
{
    internal class AlbumEntityTypeConfiguration : EntityTypeConfiguration<Album>
    {
        public AlbumEntityTypeConfiguration()
        {
            HasMany(c => c.Photos)
.WithRequired()
.HasForeignKey(c => c.AlbumID)
.WillCascadeOnDelete(false);
            ToTable("Album");
        }
    }
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
    internal class PhtotAttributeEntityTypeConfiguration : EntityTypeConfiguration<PhtotAttribute>
    {
        public PhtotAttributeEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Owner)
.WithMany()
.HasForeignKey(t => t.PhotoID)
.WillCascadeOnDelete(false);
            ToTable("PhtotAttribute");
        }
    }
}