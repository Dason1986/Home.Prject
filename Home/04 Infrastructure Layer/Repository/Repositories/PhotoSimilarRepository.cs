using System;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Linq;

namespace Home.Repository.Repositories
{
    public class PhotoSimilarRepository : Repository<PhotoSimilar>, IPhotoSimilarRepository
    {
        public PhotoSimilarRepository(EFContext context) : base(context)
        {
        }

        public bool Exist(Guid leftPhotoID, Guid rigthPhotoID)
        {
            return CreateSet().Any(n => (n.LeftPhotoID == leftPhotoID && n.RightPhotoID == rigthPhotoID) || (n.LeftPhotoID == rigthPhotoID && n.RightPhotoID == leftPhotoID));
        }
    }
}