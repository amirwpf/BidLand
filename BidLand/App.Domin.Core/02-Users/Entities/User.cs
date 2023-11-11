
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            
            UserTokens = new HashSet<UserToken>();
        }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;


        //private string Fullname;

        //public string GetFullName
        //{
        //    get { return Firstname + " " + Lastname; }
            
        //}

        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public virtual ICollection<UserLogin> Logins { get; set; }

        public virtual ICollection<UserClaim> Claims { get; set; }

    }
}
