using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AirportController : Controller
    {
        private readonly AirportService _service;
        private readonly ClientService _aux;
        public AirportController(AirportService service, ClientService aux)
        {
            _service = service;
            _aux = aux;
        }

        // Admin -----------------------------------------------------
        // GET: Airport
        //[Authorize(Policy = "Admin")]
        /*[HttpGet]
        public async Task<IActionResult> Get(string? token = null)
        {
            var airports = await _service.Get();
            return View((airports, token));
        }*/
        public async Task<IActionResult> Index()
            => View(await _service.Get());

        // POST: Airport
        [HttpPost]
        public async Task<IActionResult> Create(AirportDTO airport)
        {
            try
            {
                int airp = await _service.Create(airport);
                airport.Security.AirportID = airp;
                await _aux.Create(airport.Security, "");
                return RedirectToAction("Init", "Admin", new { sucess = "Los cambios han sido aplicados correctamente" });
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 2;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Init", "Admin", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 2;
                return RedirectToAction("Init", "Admin", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        
        // PUT: Airport
        //[Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(AirportDTO airport)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Edit(airport, userId);
                return RedirectToAction("Index", "Security");
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Index", "Security");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // DELETE: Airport/5
        [HttpPost]
        public async Task<IActionResult> Delete(string name, bool cascada)
        {
            try
            {
                await _service.Delete(name, cascada);
                return RedirectToAction("Init", "Admin", new { sucess = "Los cambios han sido aplicados correctamente" });
            }
            catch (Exception e)
            {
                int lugar_del_error = 4;
                return RedirectToAction("Init", "Admin", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        // ---------------------------------------------------------------------

    }
}
