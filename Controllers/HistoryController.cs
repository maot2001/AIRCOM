using AIRCOM.Models;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        private readonly HistoryService _service;
        public HistoryController(HistoryService service)
        {
            _service = service;
        }

        // Security-Client -------------------------------------------
        // GET: History/5
        [Authorize(Policy = "Security")]
        [Authorize(Policy = "Client")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryShips(string id)
        {
            var histories = await _service.GetHistoryShips(id);
            return View(histories);
        }
        // -----------------------------------------------------------   

        // Security --------------------------------------------------
        // GET: History
        [Authorize(Policy = "Security")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var histories = await _service.Get();
            return View(histories);
        }

        // POST: History
        [Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] History history)
        {
            var userId = HttpContext.User.FindFirst("AirportId")?.Value;
            await _service.Create(history, userId);
            return RedirectToAction(nameof(Get));
        }

        // PUT: History
        [Authorize(Policy = "Security")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] History history)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("AirportId")?.Value;
                await _service.Edit(history, userId);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: History
        [Authorize(Policy = "Security")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] History history)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("AirportId")?.Value;
                await _service.Delete(history, userId);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }
        // -----------------------------------------------------------

        // Client ----------------------------------------------------
        // PUT: History/Include
        [Authorize(Policy = "Client")]
        [HttpPut("Include")]
        public async Task<IActionResult> Include([FromBody] History history)
        {
            try
            {
                await _service.Include(history);
                return RedirectToAction(nameof(ShipsController.ClientShips), "Ships");
            }   
            catch
            {
                return NotFound();
            }
        }
        // -----------------------------------------------------------
    }
}
