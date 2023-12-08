using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Direction")]
    public class DirectionController : Controller
    {
        private readonly InstallationService _aux;
        private readonly RepairService _aux2;
        private readonly ServiceInstallationService _aux3;
        private readonly RepairInstallationService _aux4;
        private static string Name;
        private static string Airport;
        public DirectionController(InstallationService aux, RepairService aux2, ServiceInstallationService aux3, RepairInstallationService aux4)
        {
            _aux = aux;
            _aux2 = aux2;
            _aux3 = aux3;
            _aux4 = aux4;
        }
        public async Task<IActionResult> Index(int page = 1, int lugar_del_error = 0, string error = "", int? id = null, bool filter = false, string UserName="",string UserAirportName = "")
        {
            if (UserName != "") {
                Name = UserName;
            }
            if (UserAirportName != "")
            {
                Airport = UserAirportName;
            }
            ViewData["Usuario"]=Name;
            ViewData["Aeropuerto"] = Airport;
            ViewData["lugar_del_error"] = lugar_del_error;
            ViewData["error"] = error;
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var installations = await _aux.Get(userId);

            switch (page)
            {
                case 1:
                    ViewData["installations"] = installations;
                    return View("Instalaciones");
                case 2:
                    ViewData["installations"] = new SelectList(installations, "ID", "Name");
                    if (filter)
                    {
                        var services = await _aux3.Get(null, id);
                        ViewData["services"] = services;
                    }
                    else
                    {
                        var services = await _aux3.Get(userId);
                        ViewData["services"] = services;
                    }
                    return View("Servicios");
                default:
                    ViewData["installations"] = new SelectList(installations, "ID", "Name");
                    var repairs = await _aux2.Get();
                    ViewData["rep"] = new SelectList(repairs, "RepairID", "Name");
                    if (filter)
                    {
                        var ri = await _aux4.Get(null, id);
                        ViewData["repairs"] = ri;
                    }
                    else
                    {
                        var ri = await _aux4.Get(userId);
                        ViewData["repairs"] = ri;
                    }
                    return View("Reparaciones");
            }
        }
    }
}
