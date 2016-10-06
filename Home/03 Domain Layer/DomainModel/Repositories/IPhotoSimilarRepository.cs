using System;
using DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data;

namespace DomainModel.Repositories
{
    public interface IPhotoSimilarRepository : IRepository<PhotoSimilar>
    {
        bool Exist(Guid leftPhotoID, Guid rigthPhotoID);
    }
}