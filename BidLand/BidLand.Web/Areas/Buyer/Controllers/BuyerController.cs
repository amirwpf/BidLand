using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Dtos;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Threading;

namespace BidLand.Web.Areas.Buyer.Controllers
{
	[Area("Buyer")]
	//[Authorize(Roles = "Seller")]
	public class BuyerController : Controller
	{
		private readonly IPurchaseAppServices _purchaseAppServices;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IAccountAppServices _accountAppServices;
		private string username;

		public BuyerController(IPurchaseAppServices purchaseAppServices
							   , IHttpContextAccessor httpContextAccessor
							   , IAccountAppServices accountAppServices)
		{
			_purchaseAppServices = purchaseAppServices;
			_httpContextAccessor = httpContextAccessor;
			_accountAppServices = accountAppServices;
			username = _httpContextAccessor.HttpContext.User.Identity.Name;
		}

		[HttpGet]
		//[Route("/cartinfo")]
		public async Task<IActionResult> CartInfo(CancellationToken token)
		{
			if (username != null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, token);
				if (buyer != null)
				{
					var cart = await _purchaseAppServices.GetBuyerActiveCart(buyer, token);
					return View(cart);
				}
			}
			return View();
		}

		[HttpGet]

		public async Task<IActionResult> PayCart(int cartId, CancellationToken token)
		{
			if (username != null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, token);
				if (buyer != null)
				{
					var cart = await _purchaseAppServices.GetBuyerActiveCart(buyer, token);
					if (cart != null && cart.Value > buyer.Credit)
					{
						var mande = (int)cart.Value - buyer.Credit;
						ViewBag.Error = $"لطفا برای تسوه ی سبد خرید حساب خود را به مقدار {mande} شارژ کنید";
						return View();
					}
					ViewBag.CartId = cartId;
					return View();
				}
			}

			return View();
		}


		[HttpGet]

		public async Task<IActionResult> PayCartConfirm(int cartId, CancellationToken token)
		{
			if (username != null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, token);
				if (buyer != null)
				{
					var cart = await _purchaseAppServices.GetBuyerActiveCart(buyer, token);
					if (cart != null)
					{
						var result = await _purchaseAppServices.StockFixedPricePurchase(cart, token);
						if (result == "پرداخت با موفقیت صورت گرفت.")
						{
							return Redirect("/");
						}
						ViewBag.Error = result;
						return View();
					}
				}
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> AddStockToCart(CancellationToken token, [FromQuery] int quantity, [FromQuery] int stockId)
		{
			if (username != null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, token);
				if (buyer != null)
				{
					var cart = await _purchaseAppServices.GetBuyerActiveCart(buyer, token);
					if (cart != null)
					{
						var stock = await _purchaseAppServices.GetStockById(stockId, token);
						if (stock != null && stock.AvailableNumber >= quantity)
						{
							bool foundInCart = false;
							foreach (var item in cart.StocksCarts)
							{
								if (item.StockId == stockId)
								{
									var stocksCarts = await _purchaseAppServices.GetStocksCartsByCart(cart, token);
									var stocksCart = stocksCarts.FirstOrDefault(s => s.StockId == stockId);
									//var stocksCart2 = await _purchaseAppServices.GetStocksCartsById(item.Id, token);
									stocksCart.Quantity += quantity;
									foundInCart = true;
									await _purchaseAppServices.UpdateStocksCart(stocksCart, token);
									break;
								}
							}
							if (foundInCart == false)
							{
								StocksCartRepoDto stocksCartRepoDto = new StocksCartRepoDto()
								{
									StockId = stockId,
									CartId = cart.Id,
									Quantity = quantity,
									InsertionDate = DateTime.Now,
								};
								await _purchaseAppServices.AddStocksCart(stocksCartRepoDto, token);
							}
						}
					}
				}
			}
			return Redirect($"/product/stockinfo/{stockId}");
		}

		[HttpGet]
		public async Task<IActionResult> DeleteStocksCart(int id, CancellationToken cancellationToken)
		{
			var stocksCart = await _purchaseAppServices.GetStocksCartsById(id, cancellationToken);
			await _purchaseAppServices.DeleteStocksCart(stocksCart, cancellationToken);
			return Redirect("/Buyer/Buyer/CartInfo/");
		}


		[HttpGet]
		public async Task<IActionResult> AddComment(CancellationToken cancellationToken,
													[FromQuery] string title,
													[FromQuery] string desc,
													[FromQuery] string pros,
													[FromQuery] string cons,
													[FromQuery] bool isPos,
													[FromQuery] int stockId)
		{


			if (username != null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, cancellationToken);
				if (buyer != null)
				{
					CommentRepoDto commentRepoDto = new CommentRepoDto()
					{
						Advantages = pros,
						DisAdvantages = cons,
						Description = desc,
						Title = title,
						IsConfirm = null,
						StockId = stockId,
						IsPositive= isPos,
						BuyerId = buyer.Id,

					};

					await _purchaseAppServices.AddComment(commentRepoDto,cancellationToken);
				}
			}

			return Redirect($"/product/stockinfo/{stockId}");

		}


		//[HttpGet]
		//public async Task<IActionResult> EditBuyer(CancellationToken cancellationToken)
		//{
		//	if (username != null)
		//	{
		//		var user = await _accountAppServices.FindUserByEmailAsync(username);

		//		if (user != null)
		//		{
		//			return View(user);
		//		}
		//	}

		//	return Redirect("?buyer/home/index/");
		//}

		//[HttpPost]
		//public async Task<IActionResult> EditBuyer(UserDto user ,CancellationToken cancellationToken)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		await _accountAppServices.UpdateUserAsync(user);
		//	}

		//	return Redirect("?buyer/home/index/");
		//}

	}
}
