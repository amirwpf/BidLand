using App.Domin.AppServices.Users;
using App.Domin.Core._02_Users.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommentsController : Controller
    {
        private readonly IAdminPanelAppServices _adminPanelAppServices;
        public CommentsController(IAdminPanelAppServices adminPanelAppServices)
        {
            _adminPanelAppServices = adminPanelAppServices;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _adminPanelAppServices.GetPendingCommentsAsync(cancellationToken);
            return View(result);
        }
        public async Task<IActionResult> ConfirmComment(int id, bool isConfirm, CancellationToken cToken)
        {
            await _adminPanelAppServices.ConfirmCommentByIdAsync(id, isConfirm, cToken);
            return RedirectToAction("Index");
        }
    }
}
