using Home.DomainModel.ModuleProviders;
using Library.Domain.Data.EF;
using Home.DomainModel.Repositories;
using Home.Repository.Repositories;

namespace Home.Repository.ModuleProviders
{
    public class FileManagentModuleProvider : DomainModuleProvider, IFileManagentModuleProvider
    {
        public FileManagentModuleProvider(EFContext context) : base(context)
        {
        }

        public ISystemParameterRepository CreateSystemParameter()
        {
            return new SystemParameterRepository(this.Context);
        }
    }
}