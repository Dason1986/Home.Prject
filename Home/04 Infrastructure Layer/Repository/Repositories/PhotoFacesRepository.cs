
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Repository.Repositories
{
    public class PhotoFacesRepository : Repository<PhotoFace>, IPhotoFacesRepository
    {
        public PhotoFacesRepository(DbContext context) : base(context)
        {
        }

        public IList<PhotoFace> GetFacesByPhtotID(Guid photoID)
        {
            return new List<PhotoFace>();
        }
    }
}
