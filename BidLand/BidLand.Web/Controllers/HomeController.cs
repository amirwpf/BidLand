using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using BidLand.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BidLand.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IStocksCartRepository _stocksCartRepository;

		public HomeController(ILogger<HomeController> logger, IStocksCartRepository stocksCartRepository)
		{
			_logger = logger;
			_stocksCartRepository = stocksCartRepository;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var value =await _stocksCartRepository.GetCommision(cancellationToken);
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