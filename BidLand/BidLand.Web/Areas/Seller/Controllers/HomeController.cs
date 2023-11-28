﻿using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace BidLand.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
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
        public async Task<IActionResult> Index(int id , CancellationToken cancellation)
        {
            return View();
        }
        
        public async Task<IActionResult> About()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditSeller(CancellationToken cancellationToken)
        {
            if (username != null)
            {
                var user = await _accountAppServices.FindUserByEmailAsync(username);
                var seller = await _accountAppServices.FindSellerByUserId(user.Id, cancellationToken);
                return View(user);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditSeller(User model ,CancellationToken cancellationToken)
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
    }
}