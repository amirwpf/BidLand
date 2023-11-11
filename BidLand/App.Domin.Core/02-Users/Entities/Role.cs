
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {

        }
        public Role(string name) : this()
        {
            Name = name;
        }
        public Role(string name, string description) : this(name) {
            Description = description;
        }

        public string Description { get; set; }
        public virtual ICollection<UserRole> Users { get; set; }
        public virtual ICollection<RoleClaim> Claims { get; set; }
    }
}
