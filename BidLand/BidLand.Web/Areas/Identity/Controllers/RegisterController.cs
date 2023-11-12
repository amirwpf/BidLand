using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class RegisterController : Controller
    {
        private readonly IAccountAppServices _accountAppServices;
        public RegisterController(IAccountAppServices accountAppServices )
        {
            _accountAppServices = accountAppServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel model , CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _accountAppServices.FindUserByEmailAsync(model.Username);
            if (user == null)
            {
                var result = await _accountAppServices.CreateUserAsync(model,token);

            if (result.Succeeded)
            {
                return Redirect("Identity/Login");
            }
             
            }
            else {
                ModelState.AddModelError("", "قبلا با این نام کاربری ثبت نام شده است!");
            }




            return View(model);

        }

    }
}
