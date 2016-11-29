using System;
using Library.Domain.Data;
using Home.DomainModel.Aggregates.GalleryAgg;

namespace Home.DomainModel.Repositories
{
    public interface IPhotoSimilarRepository : IRepository<PhotoSimilar>
    {
        bool Exist(Guid leftPhotoID, Guid rigthPhotoID);
    }
}