using App.Domin.Core._02_Users.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name="نام")][Required(ErrorMessage="لطفا {0} را وارد نمایید!")]public string Firstname { get; set; }  
        [Display(Name="نام خانوادگی")][Required(ErrorMessage="لطفا {0} را وارد نمایید!")]public string Lastname { get; set; }  
        [Display(Name="نام کاربری - ایمیل")][Required(ErrorMessage="لطفا {0} را وارد نمایید!")]public string Username { get; set; }  
        [Display(Name="کلمه رمز")][Required(ErrorMessage="لطفا {0} را وارد نمایید!")]public string Password { get; set; }
        [Display(Name ="نوع کاربر")] public BuyerSellerTypes BuyerOrSeller { get; set; }
        [Display(Name = "تکرار کلمه رمز")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید!")]
        [Compare("Password" , ErrorMessage ="تکرار کلمه رمز یکسان نیست!")]
        public string ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }



    }
}
