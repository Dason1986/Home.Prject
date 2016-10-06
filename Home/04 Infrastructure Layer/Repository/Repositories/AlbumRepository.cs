using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Repository.Repositories
{

    public class AlbumRepository : Library.Domain.Data.EF.Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(EFContext context) : base(context)
        {
        }
    }
}