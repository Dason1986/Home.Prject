using DomainModel.DomainServices;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using Library.Domain;
using Library.Domain.Data;
using System;
using System.Linq;

namespace DomainModel.Aggregates.FileAgg
{

    public class FileAggregateRoot : AggregateRoot
    {

        public FileAggregateRoot(Guid fileid, IFileInfoRepository fileRspository = null)
        {
            if (fileRspository == null)
                fileRspository = Library.Bootstrap.Currnet.GetService<IFileInfoRepository>();
            File = fileRspository.Get(fileid);
            if (File == null) throw new Exception();
            UnitOfWork = fileRspository.UnitOfWork;
            PhotoArgs = new PhotoItemEventArgs(ID, File.Photo != null ? File.Photo.ID : Guid.Empty);
        }
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected PhotoItemEventArgs PhotoArgs { get; private set; }
        public FileAggregateRoot(FileInfo file, IFileInfoRepository fileRspository = null)
        {

            if (file == null) throw new Exception();
            File = file;
            if (fileRspository == null)
            {
                fileRspository = Library.Bootstrap.Currnet.GetService<IFileInfoRepository>();
            }
            UnitOfWork = fileRspository.UnitOfWork;
            PhotoArgs = new PhotoItemEventArgs(ID, File.Photo != null ? File.Photo.ID : Guid.Empty) ;
        }
        public FileInfo File { get; set; }

        public override Guid ID
        {
            get
            {
                return File.ID;
            }
        }



        public override void Commit()
        {
            UnitOfWork.Commit();
        }


        public void PublishPhotoDomain()
        {
            IModuleProvider ModuleProvider = Library.Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            CreatePhotoInfo();
            BuildPhotoFaces();
            BuildFingerprint();
            this.Bus.ModuleProvider = ModuleProvider;
            this.Bus.PublishAwait();
          
        }
        string[] imageExtension = { ".bmp", ".jpg", ".png", ".jpeg" };
        public bool IsImageFile()
        {
            return IsFileExtension(File.Extension, imageExtension);
        }
        string[] officeExtension = { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
        public bool IsOfficeFile()
        {
            return IsFileExtension(File.Extension, officeExtension);
        }
        private bool IsFileExtension(string extension, string[] extensions) {
            return extensions.Any(n => string.Equals(n, extension, StringComparison.OrdinalIgnoreCase));
        }
        public void CreatePhotoInfo()
        {
            AddEvent(new AddPhotoDomainEventHandler(PhotoArgs));

        }
        public void BuildFingerprint()
        {
            AddEvent(new BuildFingerprintDomainEventHandler(PhotoArgs));

        }
        public void BuildPhotoFaces()
        {
            AddEvent(new PhotoFacesDomainEventHandler(PhotoArgs));

        }
        public void SimilarPhoto()
        {
            AddEvent(new SimilarPhotoDomainEventHandler(PhotoArgs));

        }

        protected override void OnActivate()
        {

        }

        protected override void OnDeactivate(bool close)
        {

        }
    }
}