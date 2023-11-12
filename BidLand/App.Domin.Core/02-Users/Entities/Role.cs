
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
        public string? Desciption { get; set; }
    }
}
