using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    public class InstallationController : Controller
    {
        private readonly InstallationService _service;
        public InstallationController(InstallationService service)
        {
            _service = service;
        }

        // Direction -----------------------------------------------------------
        // GET: Installation
        [Authorize(Policy = "Direction")]
        [HttpGet]
        public async Task<IActionResult> Get(string token = "")
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var installations = await _service.Get(userId);
            return View((installations, token));
        }

        // POST: Installation
        [Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstallationDTO installation)
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            await _service.Create(installation, userId);
            return RedirectToAction(nameof(Get));
        }

        // PUT: Installation
        [Authorize(Policy = "Direction")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] InstallationDTO installation)
        {
            try
            {
                await _service.Edit(installation);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }

        }

        // DELETE: Installation
        [Authorize(Policy = "Direction")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] InstallationDTO installation)
        {
            try
            {
                await _service.Delete(installation);
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
