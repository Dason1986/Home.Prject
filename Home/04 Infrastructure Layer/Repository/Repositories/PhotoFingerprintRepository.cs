using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Repository.Repositories
{

    public class PhotoFingerprintRepository : Repository<PhotoFingerprint>, IPhotoFingerprintRepository
    {
        public PhotoFingerprintRepository(EFContext context) : base(context)
        {
        }

        public bool Exist(Guid phtotID, SimilarAlgorithm grayHistogram)
        {
            return Set.Any(n => n.PhotoID == phtotID && n.Algorithm == grayHistogram);
        }

        public PhotoFingerprint GetByPhtotID(Guid phtotID, SimilarAlgorithm algorithm)
        {
            return Set.FirstOrDefault(n => n.PhotoID == phtotID && n.Algorithm == algorithm);
        }

        public IList<PhotoFingerprint> GetList(SimilarAlgorithm algorithmType, int beginindex, int take)
        {
            return CreateSet().AsNoTracking().OrderBy(n=>n.Created).Where(n => n.Algorithm == algorithmType).Skip(beginindex).Take(take).ToList();
        }
    }

}