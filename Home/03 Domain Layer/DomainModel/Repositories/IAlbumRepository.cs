using Home.DomainModel.Aggregates.GalleryAgg;
using Library.ComponentModel.Model;
using Library.Domain.Data;
using Library.Domain.Data.Repositorys;
using System.Linq;

namespace Home.DomainModel.Repositories
{

    public interface IAlbumRepository : IRepository<Album>
    {
        IQueryable<Album>  GetAll(StatusCode enabled);
    }
}