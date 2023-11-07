using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._02_Users.Contracts.AppServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Threading;

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

		[HttpPost]
		public async Task<IActionResult> ConfirmProduct(int id, bool isConfirm , CancellationToken cToken) {
			await _adminPanelAppService.ConfirmProduct(id, isConfirm , cToken);
			return RedirectToAction("Index");
		}


		public async Task<IActionResult> Create() {
			var categories = await _adminPanelAppService.GetChildCategories(new CancellationToken());
			ViewBag.Categories = new SelectList(categories.Select(x=>new {x.Id , Title = x.Name}),"Id" , "Title");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductRepoDto model,CancellationToken cancellationToken	)
		{
			//if(ModelState.IsValid)
			 await _adminPanelAppService.UpdateProduct(model, cancellationToken);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(int id, CancellationToken token)
		{
			if (id > 0)
			{
				var product = await _adminPanelAppService.GetProductById(id, token);
				var categories = await _adminPanelAppService.GetChildCategories(token);
				ViewBag.Categories = new SelectList(categories.Select(x => new { x.Id, Title = x.Name }), "Id", "Title", product.CategoryId);
				return View(product);
			}else
                return RedirectToAction("Index");



        }
        [HttpPost]
		public async Task<IActionResult> Edit(ProductRepoDto model,CancellationToken cancellationToken	)
		{
			//if(ModelState.IsValid)
			 await _adminPanelAppService.UpdateProduct(model, cancellationToken);
			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            if (id > 0)
            {
                var product = await _adminPanelAppService.GetProductById(id, token);
               
               
                return View(product);
            }
            else
                return RedirectToAction("Index");



        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            //if(ModelState.IsValid)
            await _adminPanelAppService.DeleteProduct(id, cancellationToken);
            return RedirectToAction("Index");
        }


    }
}
