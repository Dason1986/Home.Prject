using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface ICreatedInfo
    {
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
    }
}
