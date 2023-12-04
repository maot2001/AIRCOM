using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize]
    public class ServiceInstallationController : Controller
    {
        private readonly ServiceInstallationService _service;
        public ServiceInstallationController(ServiceInstallationService service)
        {
            _service = service;
        }

        // Direction -----------------------------------------------------------
        // GET: ServiceInstallation
        [Authorize(Policy = "Direction")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var serviceInstallations = await _service.Get(userId);
            return View(serviceInstallations);
        }

        public async Task<IActionResult> InstalationGet(int? id)
        {
            var serviceInstallations = await _service.Get(null ,id);
            return View(serviceInstallations);
        }

        // POST: ServiceInstallation
        [Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceInstallationDTO service)
        {
            int page = 2;
            try
            {
                await _service.Create(service);
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

        // PUT: ServiceInstallation
        [Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Edit(ServiceInstallationDTO service)
        {
            int page = 2;
            try
            {
                await _service.Edit(service);
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

        // DELETE: ServiceInstallation
        [Authorize(Policy = "Direction")]
        [HttpPost]
        public async Task<IActionResult> Delete(ServiceInstallationDTO service)
        {
            int page = 2;
            try
            {
                await _service.Delete(service);
                return RedirectToAction("Index", "Direction", new { page = page });
            }
            catch (Exception e)
            {
                int lugar_del_error = 1;
                return RedirectToAction("Index", "Direction", new { page = page, lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        // ---------------------------------------------------------------------

        // Client --------------------------------------------------------------
        // GET : ServiceInstallation/Client
        [Authorize(Policy = "Client")]
        [HttpGet("GetClient")]
        public async Task<IActionResult> GetClient()
        {
            var serviceInstallations = await _service.Get();
            return View(serviceInstallations);
        }
        // ---------------------------------------------------------------------
    }
}
