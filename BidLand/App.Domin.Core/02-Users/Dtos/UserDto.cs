using App.Domin.Core._02_Users.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;  
        public string Lastname { get; set; } = string.Empty;  
        public string FullName { get; set; } = string.Empty;  
        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; set; } = new List<IdentityUserRole<int>>();
    }
}
