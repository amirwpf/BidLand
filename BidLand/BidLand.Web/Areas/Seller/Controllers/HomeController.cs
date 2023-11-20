using App.Domin.Core._02_Users.Contracts.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    //[Authorize(Roles = "Seller")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public async Task<IActionResult> About()
        {
            return View();
        }
    }
}
