using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._03_Extras.Entities;
using BidLand.Framework.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Threading;

namespace BidLand.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ProductsController : Controller
    {
        private readonly IPurchaseAppServices _purchaseAppServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IPurchaseAppServices purchaseAppServices, IWebHostEnvironment webHostEnvironment)
        {
            _purchaseAppServices = purchaseAppServices;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {

            //string userId = User.Identity.GetUserId();

            ViewBag.PendingProducts = await _purchaseAppServices.GetAllPendingProductsAsync(new CancellationToken());
            ViewBag.ConfirmedProducts = await _purchaseAppServices.GetAllConfirmedProductsAsync(new CancellationToken());

            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> ConfirmProduct(int id, bool isConfirm, CancellationToken cToken)
        {
            await _purchaseAppServices.ConfirmProduct(id, isConfirm, cToken);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Create()
        {
            var categories = await _purchaseAppServices.GetChildCategories(new CancellationToken());
            ViewBag.Categories = new SelectList(categories.Select(x => new { x.Id, Title = x.Name }), "Id", "Title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductRepoDto model, CancellationToken cancellationToken)
        {
            //string projectRootPath = _webHostEnvironment.ContentRootPath;
            //string products = Path.Combine(_webHostEnvironment.WebRootPath, "uploads\\products");
            model.Images = new List<Image>();
            if (model.ImageFile != null)
                foreach (var file in model.ImageFile)
                {
                    if (file != null && file.Length > 0)
                    {
                        var name = Path.GetFileName(file.FileName);
                        string projectRootPath = _webHostEnvironment.ContentRootPath;
                        //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/products", Guid.NewGuid() + "_" + fileName);
                        string fileName = Guid.NewGuid() + "_" + name;
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/products", fileName);
                        using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                        {
                            model.Images.Add(new Image { InsertionDate = DateTime.Now, Url = fileName });
                            await file.CopyToAsync(stream);
                        }
                    }
                }

            await _purchaseAppServices.CreateProduct(model, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            if (id > 0)
            {
                var product = await _purchaseAppServices.GetProductById(id, token);
                var categories = await _purchaseAppServices.GetChildCategories(token);
                ViewBag.Categories = new SelectList(categories.Select(x => new { x.Id, Title = x.Name }), "Id", "Title", product.CategoryId);
                return View(product);
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductRepoDto model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _purchaseAppServices.UpdateProduct(model, cancellationToken);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            if (id > 0)
            {
                var product = await _purchaseAppServices.GetProductById(id, token);


                return View(product);
            }
            else
                return RedirectToAction("Index");



        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            //if(ModelState.IsValid)
            await _purchaseAppServices.DeleteProduct(id, cancellationToken);
            return RedirectToAction("Index");
        }
        

        public async Task<IActionResult> Recover(int id, CancellationToken cancellationToken)
        {
            //if(ModelState.IsValid)
            await _purchaseAppServices.RecoverProduct(id, cancellationToken);
            return RedirectToAction("Index");
        }


    }
}
