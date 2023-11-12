using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Dtos;
using App.Domin.Core._02_Users.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class LoginController : Controller
    {
        private readonly IAccountAppServices _accountAppServices;

        public LoginController(IAccountAppServices accountAppServices)
        {
            _accountAppServices = accountAppServices;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("Home/Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _accountAppServices.FindUserByEmailAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return View(model);
            }


            var result = await _accountAppServices.SignInUserAsync(user, model.Password, model.IsPersistent, true);

            if (result.Succeeded)
            {
                var isLoggedIn = User.Identity.IsAuthenticated;
                return Redirect(model.ReturnUrl);
            }

            return View(model);

        }


        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _accountAppServices.SignOutUserAsync();

            }
            return Redirect("/");
        }
    }
}
