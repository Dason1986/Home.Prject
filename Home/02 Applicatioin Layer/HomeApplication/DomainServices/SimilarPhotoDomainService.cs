using DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.DomainEvents;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using DomainModel;
using Library.Draw.SimilarImages;
using Library;

namespace HomeApplication.DomainServices
{
    public class SimilarPhotoDomainService : PhotoDomainService, ISimilarPhotoDomainService
    {
        public SimilarPhotoDomainService()
        {
            BatchSize = 500;
            SetAlgorithm(DomainModel.SimilarAlgorithm.PerceptualHash);
        }
        private Library.Draw.SimilarImages.SimilarAlgorithm _similarImages;

        public IList<PhotoFingerprint> Fingerprints { get; set; }

        public IList<PhotoFingerprint> ComparerFingerprints { get; set; }

        public DomainModel.SimilarAlgorithm Algorithm { get; private set; }

        public int BatchSize { get; set; }
        public double Similarity
        {
            get
            {
                return _similarImages.Similarity;
            }

            set
            {
                _similarImages.Similarity = value;
            }
        }

        IPhotoSimilarRepository photoSimilarRepository;
        IPhotoFingerprintRepository photoFingerprintRepository;
        public void SetAlgorithm(DomainModel.SimilarAlgorithm type)
        {
            Algorithm = type;
            switch (Algorithm)
            {
                case DomainModel.SimilarAlgorithm.GrayHistogram:
                    _similarImages = new Library.Draw.SimilarImages.GrayHistogram();
                    break;
                case DomainModel.SimilarAlgorithm.PerceptualHash:
                    _similarImages = new Library.Draw.SimilarImages.PerceptualHash();
                    break;
                default:
                    break;
            }
        }
        protected override void CreateRepository(IGalleryModuleProvider moduleProvider)
        {
            base.CreateRepository(moduleProvider);
            photoSimilarRepository = moduleProvider.CreatePhotoSimilar();
            photoFingerprintRepository = moduleProvider.CreatePhotoFingerprint();
        }
        protected override void DoAddAction()
        {
            PhotoFingerprint current = photoFingerprintRepository.GetByPhtotID(CurrnetPhoto.ID, Algorithm);
            var filecount = photoFingerprintRepository.GetAll().Where(n => n.Algorithm == Algorithm).Count();
            IList<RangeItem<int>> ranges = new List<RangeItem<int>>();
            for (int i = 0; i < filecount; i = i + BatchSize)
            {
                var endindex = i + BatchSize;
                if (endindex >= filecount)
                {
                    endindex = filecount;
                }
                ranges.Add(new RangeItem<int>(i, endindex));

            }
            foreach (var item in ranges)
            {
                ComparerFingerprints = GetComparerFingerprints(item.Begin, item.End);
                Handle(current);
            }

        }

        void IDomainService.Handle(IDomainEventArgs args)
        {
            Handle(args as PhotoItemEventArgs);
        }
        protected virtual IList<PhotoFingerprint> GetComparerFingerprints(int beginindex, int endindex)
        {

            Logger.Trace(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);


            var take = endindex - beginindex;
            var list = photoFingerprintRepository.GetAll().Where(n => n.Algorithm == Algorithm).OrderBy(n => n.ID).Skip(beginindex).Take(take).ToList();


            return list;
        }
        void Comparer(PhotoFingerprint leftitem, PhotoFingerprint rightitem)
        {
            try
            {
                if (leftitem.ID == rightitem.ID) return;
                var result = _similarImages.Compare(leftitem.Fingerprint, rightitem.Fingerprint);
                var isSame = result.IsSame;

                if (isSame)
                {
                    if (photoSimilarRepository.Exist(leftitem.PhotoID, rightitem.PhotoID)) return;
                    Logger.Info("same:{0} - {1}", leftitem.Owner.File.FileName, rightitem.Owner.File.FileName);
                    photoSimilarRepository.Add(new DomainModel.Aggregates.GalleryAgg.PhotoSimilar(CreatedInfo.PhotoSimilar)
                    {
                        LeftPhotoID = leftitem.PhotoID,
                        RightPhotoID = rightitem.PhotoID
                    });

                    photoSimilarRepository.UnitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {

                var message = ExceptionProvider.ProvideFault(ex);
                Logger.Error(ex, string.Format("比較失敗!{0}", message));
            }
        }

        public void Handle(PhotoFingerprint photo)
        {
            foreach (var rightitem in ComparerFingerprints)
            {

                Comparer(photo, rightitem);
            }
        }

        public void InteriorComparer()
        {
            Logger.Trace("Interior Comparer");
            var row = 0;
            foreach (var leftitem in Fingerprints)
            {

                for (int i = 0; i < Fingerprints.Count; i++)
                {
                    if (i <= row) continue;
                    var rightitem = Fingerprints[i];
                    Comparer(leftitem, rightitem);


                }
                row++;
            }
        }

        public void ExternalComparer()
        {
            Logger.Trace("External Comparer");
            foreach (var leftitem in this.Fingerprints)
            {
                Handle(leftitem);

            }
        }
    }
}
