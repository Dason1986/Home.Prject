using System;
using Library.Domain.Data;
using System.Collections.Generic;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel;

namespace Home.DomainModel.Repositories
{
    public interface IPhotoFingerprintRepository : IRepository<PhotoFingerprint>
    {
        bool Exist(Guid phtotID, SimilarAlgorithm grayHistogram);
        PhotoFingerprint GetByPhtotID(Guid iD, SimilarAlgorithm algorithm);
        IList<PhotoFingerprint> GetList(SimilarAlgorithm algorithmType, int beginindex, int take);
    }
  
}