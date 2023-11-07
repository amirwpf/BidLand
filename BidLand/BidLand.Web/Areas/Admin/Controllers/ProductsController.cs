using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._02_Users.Contracts.AppServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
    
    public class ProductsController : Controller
	{
		private readonly IAdminPanelAppServices _adminPanelAppService;
		
		public ProductsController(IAdminPanelAppServices adminPanelAppServices)
		{
            _adminPanelAppService = adminPanelAppServices;
		}
		public async Task<IActionResult> Index()
		{

			string userId = User.Identity.GetUserId();
			
		 var products = await	_adminPanelAppService.GetAllProducts(new CancellationToken());

			return View(products);
		}


		public async Task<IActionResult> ConfirmProduct(int id, bool isConfirm , CancellationToken cToken) {
			await _adminPanelAppService.ConfirmProduct(id, isConfirm , cToken);
			return RedirectToAction("Index");
		}
	}
}
