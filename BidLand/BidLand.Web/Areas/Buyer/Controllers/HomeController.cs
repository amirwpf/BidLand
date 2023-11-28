using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NuGet.Common;

namespace BidLand.Web.Areas.Buyer.Controllers
{
    [Area("Buyer")]
    //[Authorize(Roles = "Seller")]
    public class HomeController : Controller
    {
        private readonly IPurchaseAppServices _purchaseAppServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountAppServices _accountAppServices;
        private string username;

        public HomeController(IPurchaseAppServices purchaseAppServices
                               , IHttpContextAccessor httpContextAccessor
                               , IAccountAppServices accountAppServices)
        {
            _purchaseAppServices = purchaseAppServices;
            _httpContextAccessor = httpContextAccessor;
            _accountAppServices = accountAppServices;
            username = _httpContextAccessor.HttpContext.User.Identity.Name;
        }
		public async Task<IActionResult> Index(CancellationToken token)
		{
			if (username != null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.GetBuyerByIdAsync(user.Id, token);
				return View(buyer);
			}
			return View();
		}


        [HttpGet]
        public async Task<IActionResult> EditBuyer(CancellationToken cancellationToken)
        {
            if (username != null)
            {
                var user = await _accountAppServices.FindUserByEmailAsync(username);
                return View(user);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditBuyer(User model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                //model.NormalizedEmail = model.Email.ToUpper();
                //model.NormalizedUserName = model.UserName.ToUpper();
                var result = await _accountAppServices.UpdateUser(model, cancellationToken);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchases(CancellationToken cancellationToken)
        {
            if (username != null)
            {
                var user = await _accountAppServices.FindUserByEmailAsync(username);
                var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, cancellationToken);
                if (buyer != null)
                {
                    var carts = await _purchaseAppServices.GetCompeleteCartsByBuyer(buyer, cancellationToken);
					return View(carts);
                }
                
            }
            return View();
        }

    }
}
