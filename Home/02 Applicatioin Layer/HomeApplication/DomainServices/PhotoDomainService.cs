using Home.DomainModel.DomainServices;
using System;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.DomainEvents;
using Home.DomainModel.Aggregates.FileAgg;
using Library.Domain.Data.ModuleProviders;

namespace HomeApplication.DomainServices
{
    public abstract class PhotoDomainService : DomainService, IPhotoDomainService
    {

        public IGalleryModuleProvider GalleryModuleProvider
        {
            get { return _moduleProvider; }
            set
            {
                _moduleProvider = value;
                if (value != null)
                {
                    CreateRepository(value);
                }
                else
                {
                    PhotoRepository = null;
                    FilesRepository = null;
                }
            }
        }

        protected override IModuleProvider Provider
        {
            get
            {
                return GalleryModuleProvider;
            }

            set
            {
                GalleryModuleProvider = value as IGalleryModuleProvider;
            }
        }

        private IGalleryModuleProvider _moduleProvider;
        protected IPhotoRepository PhotoRepository { get; set; }
        protected IFileInfoRepository FilesRepository { get; set; }

        protected virtual void CreateRepository(IGalleryModuleProvider moduleProvider)
        {
            PhotoRepository = GalleryModuleProvider.CreatePhoto();
            FilesRepository = GalleryModuleProvider.CreateFileInfo();
        }

        public Photo CurrnetPhoto { get; protected set; }
        public FileInfo CurrnetFile { get; protected set; }

        public void Handle(PhotoItemEventArgs args)
        {
            if (GalleryModuleProvider == null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.ModuleProviderNull);
            if (args == null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.PhotoItemArgumentNull, new ArgumentException("args"));
            if (args.Tag is FileInfo == false)
            {
                if (args.PhotoID == Guid.Empty && args.FileID == Guid.Empty) throw new PhotoDomainServiceException(Resources.DomainServiceResource.PhotoItemArgsNull, new ArgumentException("args"));
                CurrnetFile = FilesRepository.Get(args.FileID);
                if (CurrnetFile == null) throw new PhotoDomainServiceException(Resources.DomainServiceResource.FileInfoNotExist);
                args.Tag = CurrnetFile;
            }
            else
            {
                CurrnetFile = args.Tag as FileInfo;
            }

            if (args.PhotoID != Guid.Empty && CurrnetPhoto == null)
            {
                CurrnetPhoto = PhotoRepository.Get(args.PhotoID);
                if (CurrnetPhoto != null) CurrnetFile = CurrnetPhoto.File;
            }

            DoAddAction();
        }

        protected override void Handle(DomainEventArgs args)
        {
            Handle(args as PhotoItemEventArgs);
        }

        protected abstract void DoAddAction();

        protected override void OnDispose()
        {
            if (this.GalleryModuleProvider == null) return;

        
        }

        public virtual void Handle(FileInfo file)
        {
            throw new NotImplementedException();
        }
    }
}