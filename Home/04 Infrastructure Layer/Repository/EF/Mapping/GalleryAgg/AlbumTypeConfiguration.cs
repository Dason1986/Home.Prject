using DomainModel.Aggregates.GalleryAgg;
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
}