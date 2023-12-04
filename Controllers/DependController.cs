using AIRCOM.Models;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Admin")]
    public class DependController : Controller
    {
        private readonly DependService _service;
        public DependController(DependService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Depend depend)
        {
            try
            {
                await _service.Create(depend);
                return RedirectToAction("Init", "Admin");
            }
            catch (Exception e)
            {
                int lugar_del_error = 3;
                return RedirectToAction("Init", "Admin", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Depend depend)
        {
            try
            {
                await _service.Delete(depend);
                return RedirectToAction("Init", "Admin");
            }
            catch (Exception e)
            {
                int lugar_del_error = 6;
                return RedirectToAction("Init", "Admin", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
    }
}
