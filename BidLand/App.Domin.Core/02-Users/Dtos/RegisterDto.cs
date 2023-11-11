using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Dtos
{
    public class RegisterDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password" , ErrorMessage ="کلمه عبور یکسان نیست!")]
        public string ConfirmPassword { get; set; }    
        public string PhoneNumber { get; set; }
    }
}
