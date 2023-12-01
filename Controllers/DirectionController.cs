using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class DirectionController : Controller
    {
        public IActionResult Index(int page = 3)
        {
            switch (page)
            {
                case 1:
                    return RedirectToAction(nameof(InstallationController.Index), "Installation");
                default:
                    ViewData["page"] = page;
                    return View("Direction");
            }
        }
    }
}
