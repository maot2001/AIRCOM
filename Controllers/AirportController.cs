using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AirportController : Controller
    {
        private readonly AirportService _service;
        public AirportController(AirportService service)
        {
            _service = service;
        }

        // Admin -----------------------------------------------------
        // GET: Airport
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get(string? token = null)
        {
            var airports = await _service.Get();
            return View((airports, token));
        }
        
        // POST: Airport
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AirportDTO airport)
        {
            try
            {
                await _service.Create(airport);
                return RedirectToAction(nameof(Get));
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Error al insertar valores repetidos");
            }
            catch
            {
                return NotFound();
            }
        }
        
        // PUT: Airport
        [Authorize(Policy = "Admin")]
        [HttpPut]
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
        }

        // DELETE: Airport/5
        [Authorize(Policy = "Admin")]
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
        }
        // ---------------------------------------------------------------------

    }
}
