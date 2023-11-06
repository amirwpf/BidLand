using App.Domin.Core._01_Purchause.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;
		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}
		public IActionResult Index()
		{
		 var products = 	_productService.GetAllAsync(new CancellationToken());

			return View();
		}

	}
}
