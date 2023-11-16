using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class ServiceInstallationController : Controller
    {
        private readonly ServiceInstallationService _service;
        public ServiceInstallationController(ServiceInstallationService service)
        {
            _service = service;
        }

        // Client --------------------------------------------------------------
        // POST Valorar Servicio
        // POST Solicitar Servicio
        // ---------------------------------------------------------------------

        // Direction -----------------------------------------------------------
        // GET: ServiceInstallation
        [Authorize(Policy = "Direction")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("AirportId")?.Value;
            var servicecInstallations = await _service.Get(userId);
            return View(servicecInstallations);
        }

        // POST: ServiceInstallation
        [Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceInstallationDTO service)
        {
            try
            {
                await _service.Create(service);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: ServiceInstallation
        [Authorize(Policy = "Direction")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ServiceInstallationDTO service)
        {
            try
            {
                await _service.Edit(service);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: ServiceInstallation
        [Authorize(Policy = "Direction")]
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
    }
}
