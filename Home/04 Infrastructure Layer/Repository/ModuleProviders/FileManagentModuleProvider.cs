using Home.DomainModel.ModuleProviders;
using Library.Domain.Data.EF;
using Home.DomainModel.Repositories;
using Home.Repository.Repositories;
using System;
using System.Data.Entity;

namespace Home.Repository.ModuleProviders
{
    public class FileManagentModuleProvider : ModuleProvider, IFileManagentModuleProvider
    {
        public FileManagentModuleProvider(DbContext context) : base(context)
        {
        }

        public IFileInfoRepository CreateFileInfo()
        {
            return new FileInfoRepository(this.DbContext);
        }

        public IStorageEngineRepository CreateStorageEngine()
        {
            return new StorageEngineRepository(this.DbContext);
        }

        public ISystemParameterRepository CreateSystemParameter()
        {
            return new SystemParameterRepository(this.DbContext);
        }
    }
}