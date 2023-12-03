using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Direction")]
    public class RepairInstallationController : Controller
    {
        private readonly RepairInstallationService _service;
        public RepairInstallationController(RepairInstallationService service)
        {
            _service = service;
        }

        // Direction -----------------------------------------------------------
        // GET: RepairInstallation
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var repairInstallations = await _service.Get(userId);
            return View(repairInstallations);
        }

        // POST: RepairInstallation
        [HttpPost]
        public async Task<IActionResult> Create(RepairInstallationDTO repair)
        {
            int page = 3;
            try
            {
                await _service.Create(repair);
                return RedirectToAction("Index", "Direction", new { page = page });
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 1;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Index", "Direction", new { page = page, lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 1;
                return RedirectToAction("Index", "Direction", new { page = page, lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        // PUT: RepairInstallation
        [HttpPost]
        public async Task<IActionResult> Edit(RepairInstallationDTO repair)
        {
            int page = 3;
            try
            {
                await _service.Edit(repair);
                return RedirectToAction("Index", "Direction", new { page = page });
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 1;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Index", "Direction", new { page = page, lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 1;
                return RedirectToAction("Index", "Direction", new { page = page, lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        // DELETE: RepairInstallation
        [HttpPost]
        public async Task<IActionResult> Delete(RepairInstallationDTO repair)
        {
            int page = 3;
            try
            {
                await _service.Delete(repair);
                return RedirectToAction("Index", "Direction", new { page = page });
            }
            catch (Exception e)
            {
                int lugar_del_error = 1;
                return RedirectToAction("Index", "Direction", new { page = page, lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        // ---------------------------------------------------------------------
    }
}
