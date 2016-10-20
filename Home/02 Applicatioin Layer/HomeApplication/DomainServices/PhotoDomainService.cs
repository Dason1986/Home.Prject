using DomainModel.DomainServices;
using System;
using DomainModel.Aggregates.GalleryAgg;
using Library;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;

namespace HomeApplication.DomainServices
{

    [Serializable]
    public class PhotoDomainServiceException : LogicException
    {
        public PhotoDomainServiceException() { }
        public PhotoDomainServiceException(string message) : base(message) { }
        public PhotoDomainServiceException(string message, Exception inner) : base(message, inner) { }
        protected PhotoDomainServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
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
            if (args==null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.PhotoItemArgumentNull, new ArgumentException("args"));
            if (args.PhotoID == Guid.Empty && args.FileID == Guid.Empty) throw new PhotoDomainServiceException(Resources.DomainServiceResource.PhotoItemArgsNull, new ArgumentException("args"));
            if (ModuleProvider == null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.ModuleProviderNull);


            if (args.PhotoID != Guid.Empty)
            {
                CurrnetPhoto = PhotoRepository.Get(args.PhotoID);
                if (CurrnetPhoto != null) CurrnetFile = CurrnetPhoto.File;
            }
            if (args.FileID != Guid.Empty)
            {

                if (CurrnetFile == null)
                {
                    CurrnetFile = FilesRepository.Get(args.FileID);
                    if (CurrnetFile == null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.FileInfoNotExist);
                    CurrnetPhoto = CurrnetFile.Photo;
                }
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