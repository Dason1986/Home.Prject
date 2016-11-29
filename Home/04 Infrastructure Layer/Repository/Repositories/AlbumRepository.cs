using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Home.Repository.Repositories
{

    public class AlbumRepository : Library.Domain.Data.EF.Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(EFContext context) : base(context)
        {
        }
    }
}