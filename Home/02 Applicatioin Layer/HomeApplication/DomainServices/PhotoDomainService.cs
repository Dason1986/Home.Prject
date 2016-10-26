using DomainModel.DomainServices;
using System;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.DomainEvents;

namespace HomeApplication.DomainServices
{
    public abstract class PhotoDomainService : DomainService
    {
        //public PhotoDomainService(IGalleryModuleProvider moduleProvider)
        //{
        //    _moduleProvider = moduleProvider;
        //    CreateRepository(_moduleProvider);
        //}
        public IGalleryModuleProvider ModuleProvider
        {
            get { return _moduleProvider; }
            set
            {
                _moduleProvider = value;
                if (value != null)
                {
                    CreateRepository(value);
                }
            }

        }
        protected override IModuleProvider Provider
        {
            get
            {
                return ModuleProvider;
            }

            set
            {
                ModuleProvider = value as IGalleryModuleProvider;
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
            if (args == null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.PhotoItemArgumentNull, new ArgumentException("args"));
            if (args.Tag is DomainModel.Aggregates.FileAgg.FileInfo == false)
            {
                if (args.PhotoID == Guid.Empty && args.FileID == Guid.Empty) throw new PhotoDomainServiceException(Resources.DomainServiceResource.PhotoItemArgsNull, new ArgumentException("args"));

            }
            else
            {
                CurrnetFile = args.Tag as DomainModel.Aggregates.FileAgg.FileInfo;
                CurrnetPhoto = CurrnetFile.Photo;
            }
            if (ModuleProvider == null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.ModuleProviderNull);


            if (args.PhotoID != Guid.Empty && CurrnetPhoto == null)
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

                }
                if (CurrnetPhoto == null) CurrnetPhoto = CurrnetFile.Photo;
            }

            DoAddAction();
            //  ModuleProvider.UnitOfWork.Commit();
        }
        protected override void Handle(IDomainEventArgs args)
        {
            Handle(args as PhotoItemEventArgs);
        }
        protected abstract void DoAddAction();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ModuleProvider != null)
                {
                    this.ModuleProvider.Dispose();

                }
                base.Dispose(disposing);
            }
        }
    }
}