using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
	public class UserProfile:Entity
	{
		public UserProfile ()
		{
		}

		public Guid ContactProfileID { get; set; }

		public ContactProfile ContactProfile { get; set; }
	}

	public class ContactRole : Entity
	{
		[StringLength (20)]
		public string Name { get; set; }

		[StringLength (20)]
		public string Remark { get; set; }


		public int Level { get; set; }

		public Sex Six { get; set; }

	}
}

