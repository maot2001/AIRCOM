using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Client")]
    public class On_siteController : Controller
    {
        private readonly On_siteService _service;
        public On_siteController(On_siteService service)
        {
            _service = service;
        }

        // Client ---------------------------------------------------------------
        // GET: On_site
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            var service = await _service.Get(userId);
            return View(service);
        }

        // POST: On_site/5
        [HttpPost]
        public async Task<IActionResult> Create(ServiceInstallationDTO service)
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            await _service.Create(service, userId);
            return RedirectToAction(nameof(Get));
        }

        // PUT: On_site
        [HttpPut]
        public async Task<IActionResult> Valorate(On_siteDTO service)
        {
            await _service.Valorate(service);
            return RedirectToAction(nameof(Get));
        }

        // ----------------------------------------------------------------------
    }
}
