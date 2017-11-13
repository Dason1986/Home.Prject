using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.Data.ModuleProviders;

namespace HomeApplication.DomainServices
{

    public abstract class FileManagentDomainService : DomainService
    {
        public FileManagentDomainService()
        {
        }

        public IFileManagentModuleProvider FileModuleProvider
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
                return FileModuleProvider;
            }

            set
            {
                FileModuleProvider = value as IFileManagentModuleProvider;
            }
        }

        protected virtual void CreateRepository(IFileManagentModuleProvider moduleProvider)
        {
            FilesRepository = FileModuleProvider.CreateFileInfo();
        }

        protected IFileInfoRepository FilesRepository { get; private set; }

        private IFileManagentModuleProvider _moduleProvider;
    }
}