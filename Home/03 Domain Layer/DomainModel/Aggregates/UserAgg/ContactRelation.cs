using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
	public class ContactRelation : Entity
	{
		public ContactRelation()
		{
		}
        [StringLength(20)]
        public string Name { get; set; }
        public RelationLine Line { get; set; }
        
        public Guid LeftRoleId { get; set; }
        public Guid RightRoleId { get; set; }
       
        public int Range { get; set; }
        [StringLength(100)]
        public string Remark { get; set; }

        public virtual FamilyRole LeftRole { get; set; }
        public virtual FamilyRole RightRole { get; set; }
    }

    public enum RelationLine
    {
        None,
        Paternal,
        Maternal,
     
    }
    public enum RelationSeniority
    {
        Peer
    }
}

