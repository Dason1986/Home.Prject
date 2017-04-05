using Home.DomainModel.Repositories;
using Library.Domain.Data;

namespace Home.DomainModel.ModuleProviders
{
    public interface IFileManagentModuleProvider : IDomainModuleProvider
    {
        ISystemParameterRepository CreateSystemParameter();

        IFileInfoRepository CreateFileInfo();
    }
}