using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class InstallationController : Controller
    {
        private readonly InstallationService _service;
        public InstallationController(InstallationService service)
        {
            _service = service;
        }

        // Direction -----------------------------------------------------------
        // GET: Installation
        //[Authorize(Policy = "Direction")]
        public async Task<IActionResult> Index(string? token = null)
        {
            ViewData["page"] = 1;
            //var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var installations = await _service.Get("1");
            return View("Direction");
        }

        // POST: Installation
        //[Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Create(InstallationDTO installation)
        {
            int page = 1;
            try
            {
                //var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Create(installation, "1");
                return RedirectToAction(nameof(DirectionController.Index), "Direction", new { page = page });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: Installation
        //[Authorize(Policy = "Direction")]
        /*[HttpPut]
        public async Task<IActionResult> Edit([FromBody] InstallationDTO installation)
        {
            try
            {
                await _service.Edit(installation);
                return RedirectToAction(nameof(Get));
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: Installation
        //[Authorize(Policy = "Direction")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return RedirectToAction(nameof(Get));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }*/
        // ---------------------------------------------------------------------
    }
}
