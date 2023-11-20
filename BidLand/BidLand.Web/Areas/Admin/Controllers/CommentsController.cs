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
        private readonly IPurchaseAppServices _purchaseAppServices;
        public CommentsController(IPurchaseAppServices purchaseAppServices)
        {
            _purchaseAppServices = purchaseAppServices;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _purchaseAppServices.GetPendingCommentsAsync(cancellationToken);
            return View(result);
        }
        public async Task<IActionResult> ConfirmComment(int id, bool isConfirm, CancellationToken cToken)
        {
            await _purchaseAppServices.ConfirmCommentByIdAsync(id, isConfirm, cToken);
            return RedirectToAction("Index");
        }
    }
}
