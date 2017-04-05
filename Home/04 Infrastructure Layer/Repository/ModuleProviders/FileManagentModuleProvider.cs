using Home.DomainModel.ModuleProviders;
using Library.Domain.Data.EF;
using Home.DomainModel.Repositories;
using Home.Repository.Repositories;
using System;

namespace Home.Repository.ModuleProviders
{
    public class FileManagentModuleProvider : DomainModuleProvider, IFileManagentModuleProvider
    {
        public FileManagentModuleProvider(EFContext context) : base(context)
        {
        }

        public IFileInfoRepository CreateFileInfo()
        {
            return new FileInfoRepository(this.Context);
        }

        public ISystemParameterRepository CreateSystemParameter()
        {
            return new SystemParameterRepository(this.Context);
        }
    }
}