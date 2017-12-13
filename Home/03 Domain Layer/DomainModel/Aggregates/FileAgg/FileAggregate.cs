using Home.DomainModel.DomainServices;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library;
using Library.Domain;
using Library.Domain.Data;
using Library.Domain.Data.ModuleProviders;
using System;
using System.Linq;

namespace Home.DomainModel.Aggregates.FileAgg
{
    public class FileAggregate : IAggregate<FileInfo>
    {
        public FileAggregate(Guid fileid, IFileInfoRepository fileRspository = null)
        {
            if (fileRspository == null)
                fileRspository = Bootstrap.Currnet.GetService<IFileInfoRepository>();
            Entity = fileRspository.Get(fileid);
            if (Entity == null) throw new Exception();
            UnitOfWork = fileRspository.UnitOfWork;

        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        public FileInfo Entity { get; set; }

        public Guid ID
        {
            get
            {
                return Entity.ID;
            }
        }

        public void Commit()
        {
            UnitOfWork.Commit();
        }

        public void PublishPhotoDomain()
        {
            if (!IsImageFile()) return;

        }
        public void PublishOfficeDomain()
        {
            if (!IsOfficeFile()) return;

        }

        private static readonly string[] _imageExtension = { ".bmp", ".jpg", ".png", ".jpeg" };

        public bool IsImageFile()
        {
            return IsFileExtension(Entity.Extension, _imageExtension);
        }

        private static readonly string[] _officeExtension = { ".doc", ".docx", ".dot", ".dotx", ".xlsx", ".xltx", ".xls", ".pptx", ".potx", ".pdf" };

        public bool IsOfficeFile()
        {
            return IsFileExtension(Entity.Extension, _officeExtension);
        }

        private bool IsFileExtension(string extension, string[] extensions)
        {
            return extensions.Any(n => string.Equals(n, extension, StringComparison.OrdinalIgnoreCase));
        }

    }
}