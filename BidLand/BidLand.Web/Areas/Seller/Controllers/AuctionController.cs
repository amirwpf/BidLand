using App.Domin.AppServices.Purchase;
using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Services._01_Purchase;
using BidLand.Framework.Common;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NuGet.Common;
using System.Drawing;
using System.Threading;

namespace BidLand.Web.Areas.Seller.Controllers
{
	[Area("Seller")]
	[Authorize(Roles = "Seller")]
	public class AuctionController : Controller
	{
		private readonly IPurchaseAppServices _purchaseAppServices;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IAccountAppServices _accountAppServices;
		private string username;

		public AuctionController(IPurchaseAppServices purchaseAppServices
							   , IHttpContextAccessor httpContextAccessor
							   , IAccountAppServices accountAppServices)
		{
			_purchaseAppServices = purchaseAppServices;
			_httpContextAccessor = httpContextAccessor;
			_accountAppServices = accountAppServices;
			username = _httpContextAccessor.HttpContext.User.Identity.Name;
		}
		[HttpGet]
		public async Task<IActionResult> Index(int id, CancellationToken token)
		{
			var auctions = await _purchaseAppServices.GetAllAuction(token);
			var auction = auctions.FirstOrDefault(a => a.StockId == id && a.IsDelete == false && a.IsActive == true);
			if (auction != null)
			{
				//await _purchaseAppServices.AuctionPurchaseCompelete(auction, token);// AuctionCompelete
				ViewBag.Bids = auction.Bids;
				return View(auction);
			}
			return Redirect("/seller/booth/index");

		}

		[HttpPost]
		public async Task<IActionResult> EditAuction(AuctionRepoDto model, CancellationToken token)
		{
			#region convert
			var stockRepoDto = await _purchaseAppServices.GetStockById(model.StockId, token);
			var stock = new Stock()
			{
				AdditionalDescription = stockRepoDto.AdditionalDescription,
				//AuctionId = stockRepoDto.AuctionId,
				AvailableNumber = stockRepoDto.AvailableNumber,
				BoothId = stockRepoDto.BoothId,
				Id = stockRepoDto.Id,
				InsertionDate = stockRepoDto.InsertionDate,
				IsActive = stockRepoDto.IsActive,
				IsAuction = stockRepoDto.IsAuction,
				IsDelete = stockRepoDto.IsDelete,
				Price = stockRepoDto.Price,
				ProductId = stockRepoDto.ProductId,
				//Auction = stockRepoDto.Auction,
				Booth = stockRepoDto.Booth,
				Comments = stockRepoDto.Comments,
				Product = stockRepoDto.Product,
				Auctions = stockRepoDto.Auctions,
				StocksCarts = stockRepoDto.StocksCarts,
			};
			model.Stock = stock;
			#endregion

			if (ModelState.IsValid)
			{
				if (model.IsDelete == true || model.IsActive == false)
				{
					model.Stock.IsAuction = false;
					if(model.JobId!=null)
					{
						BackgroundJob.Delete(model.JobId);
						model.JobId = null;
					}
				}
				await _purchaseAppServices.EditAuction(model, token);
				return Redirect("/seller/booth/index");
			}
			return View(model);
		}

	}
}
