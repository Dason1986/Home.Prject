using System;

namespace DomainModel
{
	public class ContactRelation:Entity
	{
		public ContactRelation ()
		{
		}
		public Guid Left {
			get;
			set;
		}
		public Guid Right {
			get;
			set;
		}
		public Guid relationID {
			get;
			set;
		}
	}
}

