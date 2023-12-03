using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Security")]
    public class SecurityController : Controller
    {
        private readonly InstallationService _aux;
        private readonly RepairService _aux2;
        private readonly ShipsService _aux3;
        private readonly ClientService _aux4;
        private readonly HistoryService _aux5;
        public SecurityController(InstallationService aux, RepairService aux2, ShipsService aux3, ClientService aux4, HistoryService aux5)
        {
            _aux = aux;
            _aux2 = aux2;
            _aux3 = aux3;
            _aux4 = aux4;
            _aux5 = aux5;
        }

        public async Task<IActionResult> Index(int page = 4, int lugar_del_error = 0, string error = "", string? plate = null)
        {
            ViewData["page"] = page;
            ViewData["lugar_del_error"] = lugar_del_error;
            ViewData["error"] = error;
            var userId = HttpContext.User.FindFirst("Airport")?.Value;

            switch (page)
            {
                case 1:
                    ViewData["historys"] = await _aux5.Get(plate);
                    break;
                case 2:
                    ViewData["ships"] = await _aux3.Get();
                    break;
                case 3:
                    ViewData["clients"] = await _aux4.Get(userId);
                    break;
                default:
                    var installations = await _aux.Get(userId);
                    ViewData["installations"] = new SelectList(installations, "ID", "Name");
                    var repairs = await _aux2.Get();
                    ViewData["repairs"] = new SelectList(repairs, "RepairID", "Name");
                    break;
            }

            return View("Security");
        }
    }
}
