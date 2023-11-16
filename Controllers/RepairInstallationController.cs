using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class RepairInstallationController : Controller
    {
        private readonly RepairInstallationService _service;
        public RepairInstallationController(RepairInstallationService service)
        {
            _service = service;
        }

        // Direction -----------------------------------------------------------
        // GET: RepairInstallation
        [Authorize(Policy = "Direction")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("AirportId")?.Value;
            var repairInstallations = await _service.Get(userId);
            return View(repairInstallations);
        }

        // POST: RepairInstallation
        [Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RepairInstallationDTO repair)
        {
            try
            {
                await _service.Create(repair);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: RepairInstallation
        [Authorize(Policy = "Direction")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] RepairInstallationDTO repair)
        {
            try
            {
                await _service.Edit(repair);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: RepairInstallation
        [Authorize(Policy = "Direction")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RepairInstallationDTO repair)
        {
            try
            {
                await _service.Delete(repair);
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
