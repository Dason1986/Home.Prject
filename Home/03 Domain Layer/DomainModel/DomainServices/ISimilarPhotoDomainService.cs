using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.ModuleProviders;
using Library.Domain.DomainEvents;
using System;
using System.Collections.Generic;

namespace Home.DomainModel.DomainServices
{



    public class SimilarPhotoDomainEventHandler : DomainEventHandler<ISimilarPhotoDomainService>
    {
        public SimilarPhotoDomainEventHandler(PhotoItemEventArgs args) 
        {
        }
    }
    public interface ISimilarPhotoDomainService : IPhotoDomainService
    {
      
        IList<PhotoFingerprint> Fingerprints { get; set; }
        double Similarity { get; set; }
        IList<PhotoFingerprint> ComparerFingerprints { get; set; }

        void Handle(PhotoFingerprint photo);
        void InteriorComparer();
        void ExternalComparer();
    }
}