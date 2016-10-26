using DomainModel.DomainServices;
using DomainModel.ModuleProviders;
using Library.Domain;
using Library.Domain.Data;
using System;

namespace DomainModel.Aggregates.FileAgg
{

    public class FileAggregateRoot : AggregateRoot
    {
        public FileAggregateRoot(Guid fileid)
        {
            var fileRspository = Library.Bootstrap.Currnet.GetService<DomainModel.Repositories.IFileInfoRepository>();          
            File = fileRspository.Get(fileid);
            if (File == null) throw new Exception();
            UnitOfWork = fileRspository.UnitOfWork;
            PhotoArgs = new PhotoItemEventArgs(ID, File.Photo != null ? File.Photo.ID : Guid.Empty) { Tag=File};
        }
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected PhotoItemEventArgs PhotoArgs { get; private set; }
        public FileAggregateRoot(FileInfo file, IDbContext dbcontext)
        {
            UnitOfWork = dbcontext.CreateUnitOfWork();
            File = file;
            
            PhotoArgs = new PhotoItemEventArgs(ID, File.Photo != null ? File.Photo.ID : Guid.Empty) { Tag = File };
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

       
        public void PublishPhotoDomain() {
            IModuleProvider ModuleProvider = Library.Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            CreatePhotoInfo();
            BuildPhotoFaces();
            BuildFingerprint();
            SimilarPhoto();
            this.Bus.ModuleProvider = ModuleProvider;
            this.Bus.PublishAwait();
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