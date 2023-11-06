using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domin.Core._02_Users.Entities
{
	public class User : IdentityUser
	{
		public User()
		{
			UserUsedPasswords = new HashSet<UserUsedPassword>();
			UserTokens = new HashSet<UserToken>();
		}

		[StringLength(450)] public string FirstName { get; set; }

		[StringLength(450)] public string LastName { get; set; }

		[NotMapped]
		public string DisplayName
		{
			get
			{
				var displayName = $"{FirstName} {LastName}";
				return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
			}
		}
		public virtual ICollection<UserUsedPassword> UserUsedPasswords { get; set; }

		public virtual ICollection<UserToken> UserTokens { get; set; }

		public virtual ICollection<UserRole> Roles { get; set; }

		public virtual ICollection<UserLogin> Logins { get; set; }

		public virtual ICollection<UserClaim> Claims { get; set; }
	}
}
