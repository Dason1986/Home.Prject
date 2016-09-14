using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface IEntity
    {
        Guid ID { get; set; }
        StatusCode StatusCode { get; set; }
    }
}
