using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Entities
{
	public class UserUsedPassword
	{
		public int Id { get; set; }

		public string HashedPassword { get; set; }

		public virtual User User { get; set; }
		public string UserId { get; set; }
	}
}
