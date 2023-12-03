using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Direction")]
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
        [HttpPost]
        public async Task<IActionResult> Create(InstallationDTO installation)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Create(installation, userId);
                return RedirectToAction("Index", "Direction");
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 1;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Index", "Direction", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 1;
                return RedirectToAction("Index", "Direction", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        // PUT: Installation
        [HttpPost]
        public async Task<IActionResult> Edit(InstallationDTO installation)
        {
            try
            {
                await _service.Edit(installation);
                return RedirectToAction("Index", "Direction");
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 1;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Index", "Direction", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 1;
                return RedirectToAction("Index", "Direction", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        // DELETE: Installation
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return RedirectToAction("Index", "Direction");
            }
            catch (Exception e)
            {
                int lugar_del_error = 1;
                return RedirectToAction("Index", "Direction", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        // ---------------------------------------------------------------------
    }
}
