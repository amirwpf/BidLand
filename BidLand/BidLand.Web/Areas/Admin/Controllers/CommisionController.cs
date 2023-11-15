using App.Domin.Core._02_Users.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommisionController : Controller
	{
		private readonly IAdminPanelAppServices _adminPanelAppServices;
		public CommisionController(IAdminPanelAppServices adminPanelAppServices)
		{
			_adminPanelAppServices = adminPanelAppServices;
		}
		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var result = await _adminPanelAppServices.GetSellersCommision(cancellationToken);
			ViewBag.SiteCommiosion = await _adminPanelAppServices.GetSellersSumCommision(cancellationToken);
			return View(result);
		}
	}
}
