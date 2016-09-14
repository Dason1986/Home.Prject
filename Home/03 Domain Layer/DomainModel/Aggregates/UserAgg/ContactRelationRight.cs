using System;

namespace DomainModel
{
    public class ContactRelationRight : Entity
    {
        public ContactRelationRight()
        {
        }
        public Guid Left { get; set; }
        public Guid Right { get; set; }
        public Guid RelationID { get; set; }

        public virtual ContactRelation Relation { get; set; }
    }
}

