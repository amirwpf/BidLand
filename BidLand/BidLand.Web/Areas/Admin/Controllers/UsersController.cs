using App.Domin.Core._02_Users.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BidLand.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class UsersController : Controller
    {
        private readonly IAdminPanelAppServices _adminPanelAppService;
        public UsersController(IAdminPanelAppServices adminPanelAppService)
        {
            _adminPanelAppService = adminPanelAppService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
