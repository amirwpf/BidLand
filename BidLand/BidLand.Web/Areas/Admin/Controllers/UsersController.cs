using App.Domin.AppServices.Users;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;

namespace BidLand.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UsersController : Controller
    {
        private readonly IAccountAppServices _accountAppServices;
        public UsersController(IAccountAppServices accountAppServices)
        {
            _accountAppServices = accountAppServices;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SellerUsers = await _accountAppServices.GetSellerUsersAsync(new CancellationToken());
            ViewBag.BuyerUsers = await _accountAppServices.GetBuyerUsersAsync(new CancellationToken());
            return View();
        }
        public async Task<IActionResult> EditBuyer(int id, CancellationToken token)
        {

            return View(await _accountAppServices.GetBuyerByIdAsync(id, token));
        }
        [HttpPost]
        public async Task<IActionResult> EditBuyer(BuyerRepoDto model, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _accountAppServices.UpdateBuyerAsync(model, token);
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> EditSeller(int id, CancellationToken token)
        {
            return View(await _accountAppServices.GetSellerByIdAsync(id, token));

        }
        [HttpPost]

        public async Task<IActionResult> EditSeller(SellerRepoDto model, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _accountAppServices.UpdateSellerAsync(model, token);
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteBuyer(int id, CancellationToken token)
        {
            return View(await _accountAppServices.GetBuyerByIdAsync(id, token));

        }
        [HttpPost]
        public async Task<IActionResult> BuyerDeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            //if(ModelState.IsValid)
            var result = await _accountAppServices.DeleteBuyerUserAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSeller(int id, CancellationToken token)
        {
            return View(await _accountAppServices.GetSellerByIdAsync(id, token));

        }
        [HttpPost]
        public async Task<IActionResult> SellerDeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            //if(ModelState.IsValid)
         var result =    await _accountAppServices.DeleteSellerUserAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> UpdateCurrentUser() {
        //    var user = await _accountAppServices.FindUserByEmailAsync(User.Identity.Name);
        //    if (user != null) {
        //        var result = await _accountAppServices.UpdateUserAsync(new App.Domin.Core._02_Users.Dtos.UserDto() { 
        //            Id = user.Id,
        //            UserName = user.UserName,
        //            Firstname = user.Firstname + " 1",
        //            Lastname = user.Lastname + " 1",
        //            Email = user.Email,

        //        });
        //    }
        //    return View();
        //}

    }
}
