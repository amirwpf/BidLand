using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Entities
{
	public class UserLogin : IdentityUserLogin
	{
		public virtual User User { get; set; }
	}
}
