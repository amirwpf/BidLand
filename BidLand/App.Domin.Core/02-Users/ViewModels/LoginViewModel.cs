using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name="نام کاربری - ایمیل")][Required(ErrorMessage="لطفا {0} را وارد نمایید!")]public string Username { get; set; }  
        [Display(Name="کلمه رمز")][Required(ErrorMessage="لطفا {0} را وارد نمایید!")]public string Password { get; set; }
        public bool IsPersistent { get; set; }
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
