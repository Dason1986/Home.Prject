using Home.DomainModel.Aggregates.GalleryAgg;
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
.HasForeignKey(c => c.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("Photo");
        }
    }
}