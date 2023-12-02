using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    //[Authorize]
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
        //[Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AirportDTO airport)
        {
            try
            {
                int airp = await _service.Create(airport);
                airport.Security.AirportID = airp;
                await _aux.Create(airport.Security, "");
                return RedirectToAction(nameof(AdminController.Init), "Admin", new { sucess="Los cambios han sido aplicados correctamente"});
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 2;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction(nameof(AdminController.Init), "Admin", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 2;
                return RedirectToAction(nameof(AdminController.Init), "Admin", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        
        // PUT: Airport
        //[Authorize(Policy = "Admin")]
        /*[HttpPut]
        public async Task<IActionResult> Edit([FromBody] AirportDTO airport)
        {
            try
            {
                await _service.Edit(airport);
                return RedirectToAction(nameof(Get));
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Error al insertar valores repetidos");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }*/

        // DELETE: Airport/5
        //[Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string name, bool cascada)
        {
            try
            {
                await _service.Delete(name, cascada);
                return RedirectToAction(nameof(AdminController.Init), "Admin", new { sucess = "Los cambios han sido aplicados correctamente" });
            }
            catch (Exception e)
            {
                int lugar_del_error = 4;
                return RedirectToAction(nameof(AdminController.Init), "Admin", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        // ---------------------------------------------------------------------

    }
}
