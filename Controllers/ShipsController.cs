using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShipsController : Controller
    {
        private readonly ShipsService _service;
        public ShipsController(ShipsService service)
        {
            _service = service;
        }

        // Security --------------------------------------------------
        // GET: Ships
        [Authorize(Policy = "Security")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ships = await _service.Get();
            return View(ships);
        }

        // POST: Ships
        [Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShipsDTO ship)
        {
            await _service.Create(ship);
            return RedirectToAction(nameof(Get));
        }

        // PUT: Ships/5
        [Authorize(Policy = "Security")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ShipsDTO ship)
        {
            try
            {
                await _service.Edit(ship);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: Ships/5
        [Authorize(Policy = "Security")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
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
        // -----------------------------------------------------------   

        // Client ----------------------------------------------------
        // GET: Ships/Client
        [Authorize(Policy = "Client")]
        [HttpGet("Client")]
        public async Task<IActionResult> ClientShips(string token = "")
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            var ships = await _service.Get(userId);
            return View((ships, token));
        }
        // -----------------------------------------------------------   

    }
}
