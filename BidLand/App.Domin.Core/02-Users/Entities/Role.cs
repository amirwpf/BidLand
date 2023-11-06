using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Entities
{
	public class Role : IdentityRole
	{
		public Role(string name) 
		{
			Name = name;
		}

		public Role(string name, string description)
			: this(name)
		{
			Description = description;
		}

		public string Description { get; set; }

		public virtual ICollection<UserRole> Users { get; set; }

		public virtual ICollection<RoleClaim> Claims { get; set; }
	}
}
