using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    public class PhotoFace : AuditedEntity
    {
        public PhotoFace()
        {

        }
        public PhotoFace(ICreatedInfo createinfo) : base(createinfo)
        {

        }


    }
}
