using AIRCOM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AIRCOM.Controllers
{
    public class SecurityController : Controller
    {
        private readonly InstallationService _aux;
        private readonly RepairService _aux2;
        public SecurityController(InstallationService aux, RepairService aux2)
        {
            _aux = aux;
            _aux2 = aux2;
        }
        public async Task<IActionResult> Index(int page = 4)
        {
            switch (page)
            {
                case 2:
                    return RedirectToAction(nameof(ShipsController.Index), "Ships");
                case 3: 
                    return RedirectToAction(nameof(ClientController.Index), "Client");
                default:
                    ViewData["p"] = page;
                    ViewData["a"] = 0;
                    var installations = await _aux.Get("1");
                    ViewData["installations"] = new SelectList(installations, "ID", "Name");
                    var repairs = await _aux2.Get();
                    ViewData["repairs"] = new SelectList(repairs, "RepairID", "Name");
                    return View("Security");
            }
        }
    }
}
