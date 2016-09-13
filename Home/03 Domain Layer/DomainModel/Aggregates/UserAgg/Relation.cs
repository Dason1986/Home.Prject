using System;

namespace DomainModel
{
	public class Relation:Entity
	{
		public Relation ()
		{
		}

		public string Name {
			get;
			set;
		}

		public string Remark {
			get;
			set;
		}
	}
}

