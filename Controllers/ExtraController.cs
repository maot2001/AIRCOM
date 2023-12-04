using AIRCOM.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class ExtraController : Controller
    {
        private readonly ServiceInstallationService _aux;
        private readonly RepairInstallationService _aux2;
        public ExtraController (ServiceInstallationService aux, RepairInstallationService _aux2)
        {
            _aux = aux;
            _aux2 = _aux2;
        }
        public async Task<IActionResult> Index(int? id = null, int page = 1, int? code = null)
        {
            ViewData["page"] = page;
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            ViewData["airport"] = await _aux.ObtAirport(userId);

            switch (page)
            {
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
