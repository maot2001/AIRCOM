using AIRCOM.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
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

            if (page == 1)
                ViewData["repairs"] = await _aux.Get(0, "1");

            if (page == 2)
                ViewData["repairs"] = await _aux.Get(1, "1");

            if(page == 3)
                ViewData["repairs"] = await _aux.Get(2, "1");

            return View("Mechanic");
        }
    }
}
