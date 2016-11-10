﻿using DomainModel.Aggregates.GalleryAgg;
using DomainModel.ModuleProviders;
using Library.Domain.DomainEvents;
using System;
using System.Collections.Generic;

namespace DomainModel.DomainServices
{



    public class SimilarPhotoDomainEventHandler : DomainEventHandler<ISimilarPhotoDomainService>
    {
        public SimilarPhotoDomainEventHandler(PhotoItemEventArgs args) : base(args)
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