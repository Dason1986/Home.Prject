using System;
using Library.Domain.Data;
using Home.DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data.Repositorys;

namespace Home.DomainModel.Repositories
{
    public interface IPhotoSimilarRepository : IRepository<PhotoSimilar>
    {
        bool Exist(Guid leftPhotoID, Guid rigthPhotoID);
    }
}