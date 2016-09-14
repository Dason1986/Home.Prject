using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public enum StatusCode
    {
        None = 0,
        Disabled = 1,
        Enabled = 2,
        Deleted = 4
    }
}
