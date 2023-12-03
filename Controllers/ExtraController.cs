using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class ExtraController : Controller
    {
        public IActionResult Index(int page = 4)
        {
            ViewData["page"] = page;

            return View("Extra");
        }
    }
}
