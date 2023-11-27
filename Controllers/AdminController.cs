using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
         => View();
    }
}
