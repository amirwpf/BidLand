using App.Domin.AppServices.Purchase;
using App.Domin.AppServices.Users;
using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Dtos;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Entities;
using BidLand.Web.Areas.Seller.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using NuGet.Common;
using System.Runtime.InteropServices;

namespace BidLand.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    public class BoothController : Controller
    {
        private readonly IPurchaseAppServices _purchaseAppServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountAppServices _accountAppServices;
        private string username;
        public BoothController(IPurchaseAppServices purchaseAppServices
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
                var seller = await _accountAppServices.FindSellerByUserId(user.Id, token);
                var booth = await _accountAppServices.FindBoothBySellerId(seller.Id, token);
                return View(booth);
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            return View(await _purchaseAppServices.GetBoothsById(id, token));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BoothRepoDto model, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _purchaseAppServices.UpdateBoothAsync(model, token);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> AddStock(CancellationToken token, int id = 0)
        {
            var categories = await _purchaseAppServices.GetChildCategories(token);
            ViewBag.Categories = new SelectList(categories.Select(x => new { x.Id, Title = x.Name }), "Id", "Title");
            List<ProductRepoDto> products = new List<ProductRepoDto>();
            if (id != 0)
            {
                var res = await _purchaseAppServices.GetAllProductsByCategoryId(id, token);
                products = res.Where(r => r.IsActive == true && r.IsDelete == false && r.IsConfirm == true).ToList();
            }
            else
            {
                var res = await _purchaseAppServices.GetAllProducts(token);
                products = res.Where(r => r.IsActive == true && r.IsDelete == false && r.IsConfirm == true).ToList();
            }
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddFixedPriceStock(int id, CancellationToken cancellationToken)
        {

            if (username != null)
            {
                var user = await _accountAppServices.FindUserByEmailAsync(username);
                var seller = await _accountAppServices.FindSellerByUserId(user.Id, cancellationToken);
                var booth = await _accountAppServices.FindBoothBySellerId(seller.Id, cancellationToken);
                ViewBag.BoothId = booth.Id;
            }
            var product = await _purchaseAppServices.GetProductById(id, cancellationToken);
            ViewBag.ProductName = product.Name;
            ViewBag.ProductId = product.Id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFixedPriceStock(StockRepoDto model, CancellationToken cancellationToken)
        {
            if(ModelState.IsValid)
            {
                await _purchaseAppServices.AddStock(model, cancellationToken);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddAuctionStock(int id,CancellationToken cancellationToken)
        {
            ViewBag.StockId = id;
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> AddAuctionStock(AddAuctionStockVM model,CancellationToken cancellationToken)
        {
			if (ModelState.IsValid)
            {
                if(/*model.StartDate>=DateTime.Now && model.EndDate>=DateTime.Now && */model.StartDate< model.EndDate)
                {
                    var auction = new AuctionRepoDto()
                    {
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        MinimumFinalPrice = model.MinimumFinalPrice,
                        StockId = model.StockId,
                    };

                    var stock = await _purchaseAppServices.GetStockById(model.StockId,cancellationToken);
                    if(stock.IsDelete==false && stock.IsActive==true && stock.AvailableNumber>0)
                    {
                        stock.IsAuction = true;
                        await _purchaseAppServices.EditStock(stock, cancellationToken);
                        await _purchaseAppServices.AddAuction(auction, cancellationToken);
                        return RedirectToAction("Index");
					}
                    
				}
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> EditStock(int id, CancellationToken cancellationToken)
        {
            var stock = await _purchaseAppServices.GetStockById(id, cancellationToken);
            return View(stock);
        }
        [HttpPost]
        public async Task<IActionResult> EditStock(StockRepoDto model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var res = await _purchaseAppServices.EditStock(model, cancellationToken);
                if (res == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
