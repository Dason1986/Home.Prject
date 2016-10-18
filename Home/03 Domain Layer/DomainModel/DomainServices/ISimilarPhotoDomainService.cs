using DomainModel.Aggregates.GalleryAgg;
using DomainModel.ModuleProviders;
using Library.Domain.DomainEvents;
using System;
using System.Collections.Generic;

namespace DomainModel.DomainServices
{



    public class SimilarPhotoDomainEventHandler : DomainEventHandler<IAddPhotoDomainService>
    {
        public SimilarPhotoDomainEventHandler(PhotoItemEventArgs args) : base(args)
        {
        }
    }
    public interface ISimilarPhotoDomainService : IDomainService, IDomainService<PhotoItemEventArgs>
    {
        IGalleryModuleProvider ModuleProvider { get; set; }
        IList<PhotoFingerprint> Fingerprints { get; set; }
        double Similarity { get; set; }
        IList<PhotoFingerprint> ComparerFingerprints { get; set; }

        void Handle(PhotoFingerprint photo);
        void InteriorComparer();
        void ExternalComparer();
    }
}