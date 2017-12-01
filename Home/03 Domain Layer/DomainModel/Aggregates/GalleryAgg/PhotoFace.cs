using Home.DomainModel.Aggregates.ContactAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.GalleryAgg
{
    [System.ComponentModel.Description("相片面相"),
           System.ComponentModel.DisplayName("相片面相")]
    public class PhotoFace : AuditedEntity
    {
        public PhotoFace()
        {

        }
        public PhotoFace(ICreatedInfo createinfo) : base(createinfo)
        {

        }

        public Guid ContactId { get; set; }
        public virtual ContactProfile Contact { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid PhotoId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Photo Photo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }

    }
}
