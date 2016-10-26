
using DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public interface IPhotoFacesRepository : IRepository<PhotoFace>
    {
        IList<PhotoFace> GetFacesByPhtotID(Guid photoID);
    }
}
