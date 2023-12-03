using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Client")]
    public class CViewController : Controller
    {
        private readonly ShipsService _aux;
        private readonly ServiceInstallationService _aux2;

        public CViewController(ShipsService aux, ServiceInstallationService aux2)
        {
            _aux = aux;
            _aux2 = aux2;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewData["page"] = page;

            switch (page)
            {
                case 1:
                    ViewData["ships"] = await _aux.Get("4");
                    break;
                default:
                    ViewData["services"] = await _aux2.Get("1");
                    break;
            }

            return View("CView");
        }
    }
}
