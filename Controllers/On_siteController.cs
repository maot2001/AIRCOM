using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class On_siteController : Controller
    {
        private readonly On_siteService _service;
        public On_siteController(On_siteService service)
        {
            _service = service;
        }

        // Client ---------------------------------------------------------------
        // GET: On_site
        //[Authorize(Policy = "Client")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            var service = await _service.Get(userId);
            return View(service);
        }

        // POST: On_site/5
        //[Authorize(Policy = "Client")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceInstallationDTO service)
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            await _service.Create(service, userId);
            return RedirectToAction(nameof(Get));
        }

        // PUT: On_site
        //[Authorize(Policy = "Client")]
        [HttpPut]
        public async Task<IActionResult> Valorate([FromBody] On_siteDTO service)
        {
            await _service.Valorate(service);
            return RedirectToAction(nameof(Get));
        }

        // ----------------------------------------------------------------------
    }
}
