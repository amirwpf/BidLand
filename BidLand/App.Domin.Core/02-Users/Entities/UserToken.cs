using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace App.Domin.Core._02_Users.Entities
{
	public class UserToken : IdentityUserToken<string>
	{
		public virtual User User { get; set; }
	}
}
