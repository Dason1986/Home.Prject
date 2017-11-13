using Home.DomainModel.Repositories;
using Library.Domain.Data;
using Library.Domain.Data.ModuleProviders;

namespace Home.DomainModel.ModuleProviders
{
    public interface IFileManagentModuleProvider : IModuleProvider
    {
        ISystemParameterRepository CreateSystemParameter();

        IFileInfoRepository CreateFileInfo();

        IStorageEngineRepository CreateStorageEngine();
    }
}