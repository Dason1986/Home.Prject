using Home.DomainModel.ModuleProviders;
using Library.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.DomainServices
{
    public interface IOfficeFileDomainService : IFileDomainService
    {
     

        IFileManagentModuleProvider FileModuleProvider { get; set; }
    }
}
