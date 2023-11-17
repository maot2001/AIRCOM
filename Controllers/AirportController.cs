using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Get(string token = "")
        {
            var airports = await _service.Get();
            return View((airports, token));
        }
        
        // POST: Airport
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] AirportDTO airport)
        {
            try
            {
                await _service.Create(airport);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }
        
        // PUT: Airport
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit([FromBody] AirportDTO airport)
        {
            try
            {
                await _service.Edit(airport);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: Airport/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
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
