using Home.DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data;
using Library.Domain.Data.Repositorys;
using System;
using System.Collections.Generic;

namespace Home.DomainModel.Repositories
{
    public interface IPhotoFacesRepository : IRepository<PhotoFace>
    {
        IList<PhotoFace> GetFacesByPhtotID(Guid photoID);
    }
}
