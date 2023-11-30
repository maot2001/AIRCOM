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

        public IActionResult Create()
            => View();

        // POST: Airport
        //[Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AirportDTO airport)
        {
            ViewData["p"] = 1;
            try
            {
                int airp = await _service.Create(airport);
                airport.Security.AirportID = airp;
                await _aux.Create(airport.Security, "");
                ViewData["a"] = 0;
                return View("Admin");
            }
            catch (DbUpdateException e)
            {
                ViewData["a"] = 2;
                ViewData["error"] = "Error al insertar valores repetidos";
                return View("Admin");
            }
            catch (Exception e)
            {
                ViewData["a"] = 2;
                ViewData["error"] = e.Message;
                return View("Admin");
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
            ViewData["p"] = 1;
            try
            {
                await _service.Delete(name, cascada);
                ViewData["a"] = 0;
                return View("Admin");
            }
            catch (Exception e)
            {
                ViewData["a"] = 4;
                ViewData["error"] = e.Message;
                return View("Admin");
            }
        }
        // ---------------------------------------------------------------------

    }
}
