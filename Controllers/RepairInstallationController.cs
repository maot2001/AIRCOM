using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class RepairInstallationController : Controller
    {
        private readonly RepairInstallationService _service;
        public RepairInstallationController(RepairInstallationService service)
        {
            _service = service;
        }

        // Direction -----------------------------------------------------------
        // GET: RepairInstallation
        //[Authorize(Policy = "Direction")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var repairInstallations = await _service.Get(userId);
            return View(repairInstallations);
        }

        // POST: RepairInstallation
        //[Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Create(RepairInstallationDTO repair)
        {
            try
            {
                //var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Create(repair, "1");
                return RedirectToAction(nameof(DirectionController.Index), "Direction");
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 3;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction(nameof(DirectionController.Index), "Direction", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 3;
                return RedirectToAction(nameof(DirectionController.Index), "Direction", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        /*
        // PUT: RepairInstallation
        //[Authorize(Policy = "Direction")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] RepairInstallationDTO repair)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Edit(repair, userId);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: RepairInstallation
        //[Authorize(Policy = "Direction")]
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
        }*/
        // ---------------------------------------------------------------------
    }
}
