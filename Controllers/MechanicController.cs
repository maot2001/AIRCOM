using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Mechanic")]
    public class MechanicController : Controller
    {
        private readonly RepairShipService _aux;
        public MechanicController(RepairShipService aux)
        {
            _aux = aux;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewData["page"] = page;
            ViewData["a"] = false;
            var userId = HttpContext.User.FindFirst("Airport")?.Value;

            if (page == 1)
                ViewData["repairs"] = await _aux.Get(0, userId);

            if (page == 2)
                ViewData["repairs"] = await _aux.Get(1, userId);

            if(page == 3)
                ViewData["repairs"] = await _aux.Get(2, userId);

            return View("Mechanic");
        }
    }
}
