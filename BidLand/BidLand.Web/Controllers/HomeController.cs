using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._02_Users.Contracts.AppServices;
using BidLand.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BidLand.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountAppServices _accountAppServices;
		private string username;
        public HomeController(ILogger<HomeController> logger,
                              IHttpContextAccessor httpContextAccessor,
                              IAccountAppServices accountAppServices)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
			_accountAppServices = accountAppServices;
            username = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			if(username!=null)
			{
				var user = await _accountAppServices.FindUserByEmailAsync(username);
				ViewBag.UserId = user.Id;
			}
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}