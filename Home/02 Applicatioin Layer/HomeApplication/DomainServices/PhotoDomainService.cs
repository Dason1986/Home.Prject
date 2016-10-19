using DomainModel.DomainServices;
using System;
using DomainModel.Aggregates.GalleryAgg;
using Library;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;

namespace HomeApplication.DomainServices
{
    public abstract class PhotoDomainService : DomainService
    {
        public IGalleryModuleProvider ModuleProvider
        {
            get { return _moduleProvider; }

            set
            {
                if (_moduleProvider == value) return;
                _moduleProvider = value;
                if (_moduleProvider != null) CreateRepository(value);

            }
        }
        IGalleryModuleProvider _moduleProvider;
        public IPhotoRepository PhotoRepository { get; protected set; }
        public IFileInfoRepository FilesRepository { get; protected set; }
        protected virtual void CreateRepository(IGalleryModuleProvider moduleProvider)
        {
            PhotoRepository = ModuleProvider.CreatePhoto();
            FilesRepository = ModuleProvider.CreateFileInfo();
        }
        public Photo CurrnetPhoto { get; protected set; }
        public DomainModel.Aggregates.FileAgg.FileInfo CurrnetFile { get; protected set; }

        public void Handle(PhotoItemEventArgs args)
        {
            if (args.PhotoID == Guid.Empty && args.FileID == Guid.Empty) throw new Exception("無效數據");
            ModuleProvider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();

            if (args.PhotoID != Guid.Empty)
            {
                CurrnetPhoto = PhotoRepository.Get(args.PhotoID);
                if (CurrnetPhoto != null) CurrnetFile = CurrnetPhoto.File;
            }
            if (args.FileID != Guid.Empty)
            {
                CurrnetFile = FilesRepository.Get(args.FileID);
                if (CurrnetFile == null) throw new Exception("參數無效");
                CurrnetPhoto = CurrnetFile.Photo;
            }
            DoAddAction();
            ModuleProvider.UnitOfWork.Commit();
        }
        protected abstract void DoAddAction();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ModuleProvider != null)
                {
                    this.ModuleProvider.Dispose();
                    this.ModuleProvider = null;
                }
                base.Dispose(disposing);
            }
        }
    }
}