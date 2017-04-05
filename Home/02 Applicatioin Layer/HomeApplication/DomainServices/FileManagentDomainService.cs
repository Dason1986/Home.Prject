using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library.Domain.Data;

namespace HomeApplication.DomainServices
{

    public abstract class FileManagentDomainService : DomainService
    {
        public FileManagentDomainService()
        {
        }

        public IFileManagentModuleProvider ModuleProvider
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

        protected override IDomainModuleProvider Provider
        {
            get
            {
                return ModuleProvider;
            }

            set
            {
                ModuleProvider = value as IFileManagentModuleProvider;
            }
        }

        protected virtual void CreateRepository(IFileManagentModuleProvider moduleProvider)
        {
            FilesRepository = ModuleProvider.CreateFileInfo();
        }

        protected IFileInfoRepository FilesRepository { get; private set; }

        private IFileManagentModuleProvider _moduleProvider;
    }
}