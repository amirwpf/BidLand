using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Entities
{
	public class UserRole : IdentityUserRole
	{
		public virtual User User { get; set; }

		public virtual Role Role { get; set; }
	}
}
