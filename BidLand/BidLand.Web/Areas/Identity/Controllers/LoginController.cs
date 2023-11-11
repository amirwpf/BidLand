using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Identity.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
