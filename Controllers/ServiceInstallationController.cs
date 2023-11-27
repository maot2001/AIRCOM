using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ServiceInstallationController : Controller
    {
        private readonly ServiceInstallationService _service;
        public ServiceInstallationController(ServiceInstallationService service)
        {
            _service = service;
        }

        // Direction -----------------------------------------------------------
        // GET: ServiceInstallation
        //[Authorize(Policy = "Direction")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var serviceInstallations = await _service.Get(userId);
            return View(serviceInstallations);
        }

        // POST: ServiceInstallation
        //[Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceInstallationDTO service)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Create(service, userId);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: ServiceInstallation
        //[Authorize(Policy = "Direction")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ServiceInstallationDTO service)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Edit(service, userId);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: ServiceInstallation
        //[Authorize(Policy = "Direction")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ServiceInstallationDTO service)
        {
            try
            {
                await _service.Delete(service);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }
        // ---------------------------------------------------------------------

        // Client --------------------------------------------------------------
        // GET : ServiceInstallation/Client
        //[Authorize(Policy = "Client")]
        [HttpGet("GetClient")]
        public async Task<IActionResult> GetClient()
        {
            var serviceInstallations = await _service.Get();
            return View(serviceInstallations);
        }
        // ---------------------------------------------------------------------
    }
}
