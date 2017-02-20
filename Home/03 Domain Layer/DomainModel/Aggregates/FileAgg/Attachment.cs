using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.FileAgg
{
    public abstract class Attachment : CreateEntity
    {
        public Attachment()
        {

        }
        public Attachment(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        public Guid FileID { get; set; }

        public virtual FileInfo File { get; set; }
    }
}
