using App.Domin.AppServices.Users;
using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.AppServices;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidLand.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class BoothsController : Controller
    {
        private readonly IAdminPanelAppServices _adminPanelAppServices;
        public BoothsController(IAdminPanelAppServices adminPanelAppServices)
        {
            _adminPanelAppServices = adminPanelAppServices;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            List<BoothRepoDto> boothList = await _adminPanelAppServices.GetAllBooths(token);
            return View(boothList);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken token)
        {
            return View(await _adminPanelAppServices.GetBoothsById(id, token));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BoothRepoDto model, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _adminPanelAppServices.UpdateBoothAsync(model, token);
                return RedirectToAction("Index");
            }
               return View();
        }
    }

}
