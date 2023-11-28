using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Entities;
using BidLand.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using NuGet.Common;

namespace BidLand.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IPurchaseAppServices _purchaseAppServices;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IAccountAppServices _accountAppServices;
		private string username;

		public ProductController(IPurchaseAppServices purchaseAppServices
							   , IHttpContextAccessor httpContextAccessor
							   , IAccountAppServices accountAppServices)
		{
			_purchaseAppServices = purchaseAppServices;
			_httpContextAccessor = httpContextAccessor;
			_accountAppServices = accountAppServices;
			username = _httpContextAccessor.HttpContext.User.Identity.Name;
		}
		public async Task<IActionResult> Index(CancellationToken cancellationToken,[FromQuery] int currentPage=1 , [FromQuery] int pageSize = 3,
											  [FromQuery] string sortBy="name" , [FromQuery] int category = 1)
		{
			var products = await _purchaseAppServices.GetAllProducts(cancellationToken);
			var count = products.Count();
			var pageCount = MathF.Ceiling((float)count / (float)pageSize);
			var currentProducts = products.Skip(pageSize*(currentPage-1)).Take(pageSize).ToList();
			if(sortBy=="name")
			{
				currentProducts = currentProducts.OrderBy(p => p.Name).ToList();
			}
			if(sortBy=="price")
			{
                currentProducts = currentProducts.OrderBy(p => p.BasePrice).ToList();
            }
			if(category!=1)
			{
				currentProducts = currentProducts.Where(p=>p.CategoryId==category).ToList();
			}
			ViewBag.PageCount =pageCount;
			ViewBag.PageSize =pageSize;
			ViewBag.CurrentPage = currentPage;
			var categories = await _purchaseAppServices.GetChildCategories(cancellationToken);
			ViewBag.Categories = new SelectList(categories.Select(x => new { x.Id, Title = x.Name }), "Id", "Title");
			ProductIndexVM productIndexVM = new ProductIndexVM()
			{
				Products = currentProducts,
				PageSize = pageSize,
				CategoryId = category,
				SortBy = sortBy,
			};
			return View(productIndexVM);
		}

		[HttpPost]
		public async Task<IActionResult> Index(ProductIndexVM productIndexVM)
		{
			var rv = new
			{
				currentPage = 1,
				pageSize = productIndexVM.PageSize,
				sortBy = productIndexVM.SortBy,
				category = productIndexVM.CategoryId,
			};
			return RedirectToAction("index", rv);
		}

		[HttpGet]
		public async Task<IActionResult> ProductInfo(int id,CancellationToken cancellationToken)
		{
			var product = await _purchaseAppServices.GetProductById(id, cancellationToken);
			var stocks = await _purchaseAppServices.GetAllStocks(cancellationToken);
			var selectedStocks = stocks.Where(s=>s.ProductId == product.Id).ToList();
			ProductInfoVM productInfoVM = new ProductInfoVM()
			{
				Stocks = selectedStocks,
				Product = product,
			};
			return View(productInfoVM);
		}

		[HttpGet]
		public async Task<IActionResult> StockInfo(int id, CancellationToken cancellationToken)
		{
			var stocks = await _purchaseAppServices.GetStockById(id,cancellationToken);
			if (username != null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, cancellationToken);
				if (buyer != null)
				{
					var carts = await _purchaseAppServices.GetCompeleteCartsByBuyer(buyer, cancellationToken);
					List<int> stockIdList =new List<int>();
					foreach (var cart in carts)
					{
						foreach (var stocksCart in cart.StocksCarts)
						{
							stockIdList.Add(stocksCart.StockId);
						}
					}

					if(stockIdList.Contains(id))
					{
						ViewBag.HasBought = true;
					}
					else
					{
						ViewBag.HasBought = false;
					}
				}
			}
			var coments = await _purchaseAppServices.GetAllComments(cancellationToken);
			var SelectedComments = coments.Where(x=>x.StockId==id && x.IsConfirm==true ).ToList();
			ViewBag.Comments=SelectedComments;
			return View(stocks);
		}

		[HttpGet]
		[Route("/boothlist")]
		public async Task<IActionResult> BoothList(CancellationToken token)
		{
			List<BoothRepoDto> boothList = await _purchaseAppServices.GetAllBooths(token);
			var booths = boothList.Where(b=>b.IsDelete==false).ToList();
			return View(booths);
		}

		[HttpGet]
		public async Task<IActionResult> BoothDetail(int id,CancellationToken token)
		{
			var booth = await _purchaseAppServices.GetBoothsById(id,token);
			return View(booth);
		}

		[HttpGet]
		public async Task<IActionResult> AuctionDetail(CancellationToken token ,[FromQuery] int stockId, [FromQuery]  int boothId)
		{
			var auctions = await _purchaseAppServices.GetAllAuction(token);
			var auction = auctions.Where(a => a.StockId == stockId).ToList();
			var auc = auction.FirstOrDefault(a=>a.Stock.BoothId== boothId && a.IsDelete==false && a.IsActive==true);
			return View(auc);
		}
		
		[HttpGet]
		[Route("/auctionlist")]
		public async Task<IActionResult> AuctionList(int id, CancellationToken token)
		{
			var stocks = await _purchaseAppServices.GetAllStocks(token);
			var autionList = stocks.Where(s=>s.IsAuction==true).ToList();
			return View(autionList);
		}

		[HttpGet]
		public async Task<IActionResult> AddBid(int id, CancellationToken token)
		{
			if(username!=null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				var buyer = await _accountAppServices.FindBuyerByUserId(user.Id, token);
				if(buyer!=null)
				{
					BidRepoDto bid = new BidRepoDto()
					{
						AuctionId = id,
						BuyerId = buyer.Id,
					};
					return View(bid);
				}
			}
			return View();
			
		}

		[HttpPost]
		public async Task<IActionResult> AddBid(BidRepoDto model, CancellationToken token)
		{
			if(ModelState.IsValid)
			{
				var buyer = await _accountAppServices.FindBuyerById((int)model.BuyerId, token);
				var auction = await _purchaseAppServices.GetAuctionById((int)model.AuctionId, token);
				if(buyer.Credit>=model.Price)
				{
					await _purchaseAppServices.AddBid(model, token);
					return Redirect($"/product/auctiondetail?stockid={auction.StockId}&boothid={auction.Stock.BoothId}");
				}
				else
				{
					ViewBag.ErrorMessage = "لطفا برای پیشنهاد این قیمت حساب خود را شارژ نمایید.";
					return View(model);
				}
			}
			return View(model);
		}
	}
}
