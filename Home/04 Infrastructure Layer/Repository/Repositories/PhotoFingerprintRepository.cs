using System;
using System.Collections.Generic;
using System.Linq;
using Home.DomainModel;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Data.Entity;

namespace Home.Repository.Repositories
{

    public class PhotoFingerprintRepository : Repository<PhotoFingerprint>, IPhotoFingerprintRepository
    {
        public PhotoFingerprintRepository(DbContext context) : base(context)
        {
        }

        public bool Exist(Guid phtotID, SimilarAlgorithm grayHistogram)
        {
            return Wrapper.Find().Any(n => n.PhotoID == phtotID && n.Algorithm == grayHistogram);
        }

        public PhotoFingerprint GetByPhtotID(Guid phtotID, SimilarAlgorithm algorithm)
        {
            return Wrapper.Find().FirstOrDefault(n => n.PhotoID == phtotID && n.Algorithm == algorithm);
        }

        public IList<PhotoFingerprint> GetList(SimilarAlgorithm algorithmType, int beginindex, int take)
        {
            return Wrapper.Find().AsNoTracking().OrderBy(n=>n.Created).Where(n => n.Algorithm == algorithmType).Skip(beginindex).Take(take).ToList();
        }
    }

}