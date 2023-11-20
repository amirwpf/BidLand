using App.Domin.Core._02_Users.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommisionController : Controller
	{
		private readonly IPurchaseAppServices _purchaseAppServices;
		public CommisionController(IPurchaseAppServices purchaseAppServices)
		{
			_purchaseAppServices = purchaseAppServices;
		}
		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var result = await _purchaseAppServices.GetSellersCommision(cancellationToken);
			ViewBag.SiteCommiosion = await _purchaseAppServices.GetSellersSumCommision(cancellationToken);
			return View(result);
		}
	}
}
