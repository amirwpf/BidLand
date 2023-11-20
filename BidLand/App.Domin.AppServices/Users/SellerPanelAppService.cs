using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Services;
using App.Domin.Core._03_Extras.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.AppServices.Users;

public class SellerPanelAppService : ISellerPanelAppService
{

	private readonly IProductService _productServices;
	private readonly ICommentService _commentServices;
	private readonly ICategoryService _categoryService;
	private readonly IBoothService _boothService;
	private readonly IBuyerService _buyerService;
	private readonly ISellerService _sellerService;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IStocksCartService _stocksCartService;


	public SellerPanelAppService(IProductService productService,
								ICommentService commentService,
								ICategoryService categoryService,
								IBoothService boothService,
								IStocksCartService stocksCartService,
								IHttpContextAccessor httpContextAccessor,
								ISellerService sellerService,
								IBuyerService buyerService)
	{
		_productServices = productService;
		_commentServices = commentService;
		_categoryService = categoryService;
		_boothService = boothService;
		_httpContextAccessor = httpContextAccessor;
		_sellerService = sellerService;
		_buyerService = buyerService;
		_stocksCartService = stocksCartService;
	}

	public async Task<SellerRepoDto?> FindSellerByUserId(int UserId, CancellationToken cancellation)
	{
		var sellers = await _sellerService.GetAllAsync(cancellation);
		if (sellers != null)
		{
			var seller = sellers.FirstOrDefault(x => x.UserId == UserId);
			return seller;
		}
		return null;
	}
	public async Task<BoothRepoDto?> FindBoothBySellerId(int SellerId, CancellationToken cancellation)
	{
		var booths = await _boothService.GetAllAsync(cancellation);
		if (booths != null)
		{
			var booth = booths.FirstOrDefault(x => x.SellerId == SellerId);
			return booth;
		}
		return null;
	}

	public async Task<List<BoothRepoDto>> GetAllBooths(CancellationToken token)
	{
		return await _boothService.GetAllAsync(token);
	}

	public async Task<BoothRepoDto> GetBoothsById(int id, CancellationToken token)
	{
		return await _boothService.GetByIdAsync(id, token);
	}
}
