using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Services;
using App.Domin.Core._02_Users.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Services;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._03_Extras.Enums;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domin.AppServices.Purchase
{
	public class PurchaseAppServices : IPurchaseAppServices
	{
		private readonly IProductService _productServices;
		private readonly ICommentService _commentServices;
		private readonly ICategoryService _categoryService;
		private readonly IBoothService _boothService;
		private readonly IStocksCartService _stocksCartService;
		private readonly IStockService _stockService;
		private readonly IAuctionService _auctionService;
		private readonly IBidService _bidService;
		private readonly IAdminService _adminService;
		private readonly IBuyerService _buyerService;
		private readonly ISellerService _sellerService;
		private readonly IMedalService _medalService;


		public PurchaseAppServices(IProductService productService,
									ICommentService commentService,
									ICategoryService categoryService,
									IBoothService boothService,
									IStocksCartService stocksCartService,
									IStockService stockService,
									IAuctionService auctionService,
									IBidService bidService,
									IAdminService adminService,
									IBuyerService buyerService,
									ISellerService sellerService,
									IMedalService medalService)
		{
			_productServices = productService;
			_commentServices = commentService;
			_categoryService = categoryService;
			_boothService = boothService;
			_stocksCartService = stocksCartService;
			_stockService = stockService;
			_auctionService = auctionService;
			_bidService = bidService;
			_adminService = adminService;
			_buyerService = buyerService;
			_sellerService = sellerService;
			_medalService = medalService;
		}

		#region ProductsManagement
		public async Task<List<ProductRepoDto>> GetAllProducts(CancellationToken cancellationToken)
		{
			return await _productServices.GetAllAsync(cancellationToken);
		}

		public async Task<List<ProductRepoDto>> GetAllProductsByCategoryId(int id, CancellationToken cancellationToken)
		{
			return await _productServices.GetAllWithCategoryId(id, cancellationToken);
		}

		public async Task<bool> ConfirmProduct(int productId, bool isConfirm, CancellationToken cancellationToken)
		{
			return await _productServices.ConfirmProductAsync(productId, isConfirm, cancellationToken);
		}

		public async Task<List<ProductRepoDto>> GetAllConfirmedProductsAsync(CancellationToken cancellationToken)
		{
			return await _productServices.GetAllConfirmedProductsAsync(cancellationToken);
		}
		public async Task<List<ProductRepoDto>> GetAllPendingProductsAsync(CancellationToken cancellationToken)
		{
			return await _productServices.GetAllPendingProductsAsync(cancellationToken);
		}

		#endregion



		#region Auction


		public async Task<string> AuctionPurchaseSuccessfull(AuctionRepoDto auction, StockRepoDto? stockDto, List<BidRepoDto> AllBidDto
			, CancellationToken cancellation)
		{
			try
			{
				//check stock exist
				if (stockDto == null) return "کالا نامعتبر";

				//check bids for this Auction
				if (AllBidDto == null) return "هیچ پیشنهادی وجود ندارد.";
				var bidDtos = AllBidDto.Where(b => b.AuctionId == auction.Id).ToList();
				if (bidDtos == null) return "هیچ پیشنهادی برای این حراجی وجود ندارد.";

				//select the highest bid
				var bid = bidDtos.OrderByDescending(b => b.Price).First();

				//check stock validation
				if (stockDto.IsDelete == true || stockDto.IsActive == false || stockDto.IsAuction == false) return "کالا نامعتبر";

				//check stock avaiable numbger is enough
				if (stockDto.AvailableNumber < 1) return "موجودی کالا کافی نیست.";

				//check bid price is enough for Auction minimum price
				if (bid.Price < auction.MinimumFinalPrice) return "هیچ پیشنهادی به حد نصاب حراجی نرسیده است.";

				//check buyer validation
				if (bid.Buyer.IsDelete == true || bid.Buyer.IsBan == true || bid.Buyer.Credit < bid.Price) return "خریدار نامعتبر";

				//check booth validation
				if (stockDto.Booth.IsDelete == true || stockDto.Booth.Seller.IsDelete == true || stockDto.Booth.Seller.IsBan == true
					|| stockDto.Booth.Seller.IsActive == false) return "فروشگاه نامعتبر";


				//set the bid as winner bid for this auction
				await _bidService.BidWon(bid, cancellation);

				//calculate commision value for this auction
				var commisionValue = await _stocksCartService.GetSTockCommisionValue(stockDto, cancellation);
				
				//chech commision calculation
				if (commisionValue == null) return "خطایی در محاسبه کارمزد رخ داد.";

				// Add sales value to seller account
				await _stockService.AddSalesValueToSeller(stockDto, bid.Price, commisionValue, cancellation);

				// subtract sales value from buyer account
				await _bidService.SubtractSalesValueFromBuyer(bid, bid.Price, cancellation);

				// Add sales commision value to site wallet
				await _adminService.AddCommisionValueToAdmin(commisionValue, cancellation);


				//Update seller medal
				var seller = await _sellerService.GetByIdAsync((int)stockDto.Booth.SellerId, cancellation);
				await UpdateSellerMedal(seller, cancellation);

				return "پرداخت با موفقیت صورت گرفت.";
			}
			catch (Exception e)
			{
				return e.ToString();
			}

		}



		public async Task AddAuction(AuctionRepoDto model, CancellationToken cancellationToken)
		{
			var auction = _auctionService.CreateAsync(model, cancellationToken).Result;
			var datetime = (DateTime)auction.EndDate;
			var ts = new DateTimeOffset(datetime);
			var res = BackgroundJob.Schedule<PurchaseAppServices>(
				x => x.GetAuctionById2(auction.Id, cancellationToken), ts);
			auction.JobId = res;
			await _auctionService.UpdateAsync(auction,cancellationToken);
		}
		public async Task EditAuction(AuctionRepoDto model, CancellationToken cancellationToken)
		{
			await _auctionService.UpdateAsync(model, cancellationToken);
		}
		public async Task<AuctionRepoDto> GetAuctionById(int id, CancellationToken cancellationToken)
		{
			return await _auctionService.GetByIdAsync(id, cancellationToken);
			
		}
		
		public async Task GetAuctionById2(int id, CancellationToken cancellationToken)
		{
			var res = await _auctionService.GetByIdAsync(id, cancellationToken);
			await AuctionPurchaseCompelete(res, cancellationToken);
		}
		public async Task<List<AuctionRepoDto>> GetAllAuction(CancellationToken cancellationToken)
		{
			return await _auctionService.GetAllAsync(cancellationToken);
		}

		public async Task<string> AuctionPurchaseCompelete(AuctionRepoDto auction, CancellationToken cancellationToken)
		{
			try
			{
				var stockDto = await _stockService.GetByIdAsync(auction.StockId, cancellationToken);
				var AllBidDto = await _bidService.GetAllAsync(cancellationToken);

				//check for auction successition
				var result = await AuctionPurchaseSuccessfull(auction, stockDto, AllBidDto, cancellationToken);


				// remove Auction from stock and go to fixed price sale mode
				if (stockDto != null)
				{
					stockDto.IsAuction = false;
					stockDto.Auctions.FirstOrDefault(a => a.Id == auction.Id).IsActive = false;
					await _stockService.UpdateAsync(stockDto, cancellationToken);
				}
				return result;
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
			

		}

		#endregion

		#region Stocks

		public async Task AddStock(StockRepoDto model, CancellationToken cancellationToken)
		{
			await _stockService.CreateAsync(model, cancellationToken);
		}

		public async Task<StockRepoDto?> GetStockById(int id, CancellationToken cancellationToken)
		{
			return await _stockService.GetByIdAsync(id, cancellationToken);
		}

		public async Task<bool?> EditStock(StockRepoDto model, CancellationToken cancellationToken)
		{
			return await _stockService.UpdateAsync(model, cancellationToken);
		}


		public async Task<string> StockFixedPricePurchase(CartRepoDto cartDto, CancellationToken cancellationToken)
		{
			try
			{
				//check for cart validation
				if (cartDto == null) return "سبد خرید نامعتبر";
				if (cartDto.StocksCarts == null) return "سبد خرید نامعتبر";
				var stocks = new List<StockRepoDto>();
				var buyer = await _buyerService.GetByIdAsync((int)cartDto.BuyerId, cancellationToken);

				foreach (var item in cartDto.StocksCarts)
				{
					var temp = await _stockService.GetByIdAsync(item.StockId, cancellationToken);
					stocks.Add(temp);
				}
				//check for stocks validation
				if (stocks == null) return "کالایی در سبد خرید وجود ندارد.";

				foreach (var stockDto in stocks)
				{
					//check for each stock validation
					if (stockDto.IsDelete == true || stockDto.IsActive == false || stockDto.IsAuction == false) return "کالا نامعتبر";

					//check for each stock available number
					if (stockDto.AvailableNumber < 1) return "موجودی کالا نا کافی می باشد.";

					//check for each stock booth validation
					if (stockDto.Booth.IsDelete == true || stockDto.Booth.Seller.IsDelete == true || stockDto.Booth.Seller.IsBan == true
						|| stockDto.Booth.Seller.IsActive == false) return "فروشگاه نامعتبر";

					//check for each stock  seller commision
					var commisionValue = await _stocksCartService.GetSTockCommisionValue(stockDto, cancellationToken);
					if (commisionValue == null) return "خطایی در پرداخت ایجاد شد";


					// Add sales value to seller account
					await _stockService.AddSalesValueToSeller(stockDto, stockDto.Price, commisionValue, cancellationToken);

					// Subtract sales value from Buyer account
					await _buyerService.SubtractSalesValueFromBuyer(buyer, stockDto.Price, cancellationToken);

					// Add sales commision value to site wallet
					await _adminService.AddCommisionValueToAdmin(commisionValue, cancellationToken);

					//Update seller medal
					var seller = await _sellerService.GetByIdAsync((int)stockDto.Booth.SellerId, cancellationToken);
					await UpdateSellerMedal(seller, cancellationToken);
				}


				return "پرداخت با موفقیت صورت گرفت.";
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		#endregion



		#region CommentsManagement


		public async Task<List<CommentRepoDto>> GetPendingCommentsAsync(CancellationToken cancellationToken)
		{
			return await _commentServices.GetAllCommentsWithSellerNameConfirmAsync(cancellationToken);
		}
		public async Task<bool> ConfirmCommentByIdAsync(int commentId, bool isConfirm, CancellationToken cancellationToken)
		{
			return await _commentServices.ConfirmCommentByIdAsync(commentId, isConfirm, cancellationToken);

		}
		#endregion


		#region Categories
		public async Task<List<CategoryRepoDto>> GetChildCategories(CancellationToken cancellationToken)
		{
			return await _categoryService.GetAllChildAsync(cancellationToken);
		}

		public async Task CreateProduct(ProductRepoDto model, CancellationToken cancellationToken)
		{

			model.InsertionDate = DateTime.Now;
			await _productServices.CreateAsync(model, cancellationToken);
		}

		public Task<ProductRepoDto> GetProductById(int id, CancellationToken cancellationToken)
		{
			return _productServices.GetByIdAsync(id, cancellationToken);
		}

		public async Task<bool> UpdateProduct(ProductRepoDto model, CancellationToken cancellationToken)
		{
			model.InsertionDate = DateTime.Now;
			return await _productServices.UpdateAsync(model, cancellationToken);
		}

		public async Task<bool> DeleteProduct(int id, CancellationToken cancellationToken)
		{
			return await _productServices.DeleteAsync(id, cancellationToken);
		}

		public async Task<bool> RecoverProduct(int id, CancellationToken cancellationToken)
		{
			return await _productServices.RecoverAsync(id, cancellationToken);
		}

		public async Task<List<BoothRepoDto>> GetAllBooths(CancellationToken token)
		{
			return await _boothService.GetAllAsync(token);
		}

		public async Task<BoothRepoDto> GetBoothsById(int id, CancellationToken token)
		{
			return await _boothService.GetByIdAsync(id, token);
		}

		public async Task<BoothRepoDto> UpdateBoothAsync(BoothRepoDto model, CancellationToken token)
		{
			model.InsertionDate = DateTime.Now;
			await _boothService.UpdateAsync(model, token);
			return model;
		}

		#endregion


		#region Seller

		public async Task<List<SellerCommissionDto?>> GetSellersCommision(CancellationToken cancellationToken)
		{
			return await _stocksCartService.GetSellersCommision(cancellationToken);
		}

		public async Task<float?> GetSellersSumCommision(CancellationToken cancellationToken)
		{
			return await _stocksCartService.GetSellersSumCommision(cancellationToken);
		}

		public async Task<bool> UpdateSellerMedal(SellerRepoDto seller, CancellationToken cancellationToken)
		{
			var medalBronze = await _medalService.GetMedalByNameAsync(MedalEnum.Bronze, cancellationToken);
			var medalSilver = await _medalService.GetMedalByNameAsync(MedalEnum.Silver, cancellationToken);
			var medalGold = await _medalService.GetMedalByNameAsync(MedalEnum.Gold, cancellationToken);
			if (seller != null)
			{
				if (seller.SalesAmount <= 100000)
				{
					seller.MedalId = medalBronze.Id;
				}
				else if (seller.SalesAmount <= 200000 && seller.SalesAmount > 100000)
				{
					seller.MedalId = medalSilver.Id;
				}
				else if (seller.SalesAmount > 200000)
				{
					seller.MedalId = medalGold.Id;
				}
				await _sellerService.UpdateAsync(seller, cancellationToken);
				return true;
			}
			return false;
		}

		#endregion
	}
}
