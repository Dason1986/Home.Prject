using System;
using DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data;

namespace DomainModel.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void DeletePhotoAllInfoByID(Guid iD);
    }
}
