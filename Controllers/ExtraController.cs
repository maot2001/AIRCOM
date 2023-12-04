using AIRCOM.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class ExtraController : Controller
    {
        private readonly ServiceInstallationService _aux;
        private readonly RepairInstallationService _aux2;
        private readonly On_siteService _aux3;
        public ExtraController (ServiceInstallationService aux, RepairInstallationService aux2, On_siteService aux3)
        {
            _aux = aux;
            _aux2 = aux2;
            _aux3 = aux3;
        }
        public async Task<IActionResult> Index(int? id = null, int page = 1, int? code = null, bool check = false, string error = "")
        {
            ViewData["page"] = page;
            ViewData["check"] = check;
            ViewData["error"] = error;
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var Id = HttpContext.User.FindFirst("Id")?.Value;
            if (userId == "0")
            {
                int air = await _aux3.GetAir(code);
                userId = air.ToString();
            }
            ViewData["airport"] = await _aux.ObtAirport(userId);

            switch (page)
            {
                case 1:
                    ViewData["service"] = await _aux3.GetOne(code, Id);
                    break;
                case 2:
                    ViewData["serviceInst"] = await _aux.GetComments(userId, code);
                    break;
                case 3:
                    ViewData["repairInst"] = await _aux2.GetComments(userId, id);
                    break;
                default:
                    break;
            }
            return View("Extra");
        }
    }
}
