
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using Library.Domain.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PhotoFacesRepository : Repository<PhotoFace>, IPhotoFacesRepository
    {
        public PhotoFacesRepository(EFContext context) : base(context)
        {
        }

        public IList<PhotoFace> GetFacesByPhtotID(Guid photoID)
        {
            return new List<PhotoFace>();
        }
    }
}
