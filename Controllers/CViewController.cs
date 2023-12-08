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
        private static string ClientName;
        public CViewController(ShipsService aux, ServiceInstallationService aux2)
        {
            _aux = aux;
            _aux2 = aux2;
        }
        public async Task<IActionResult> Index(int page = 1,string UserName = "")
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            if(UserName!="")
            {
                ClientName = UserName;
            }
            ViewData["Usuario"] = ClientName;
            switch (page)
            {
                case 1:
                    ViewData["ships"] = await _aux.Get(userId);
                    return View("Naves");
                default:
                    ViewData["services"] = await _aux2.Get();
                    return View("Servicios");
            }
        }
    }
}
