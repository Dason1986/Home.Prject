using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Data.Entity;
using Library.ComponentModel.Model;
using System.Linq;

namespace Home.Repository.Repositories
{

    public class AlbumRepository : Library.Domain.Data.EF.Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<Album> GetAll(StatusCode enabled)
        {
            return Wrapper.Find().Where(n => n.StatusCode == enabled);
        }
    }
}