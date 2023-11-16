using AIRCOM.Models;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly ClientService _service;
        public ClientController(ClientService service)
        {
            _service = service;
        }

        // Security --------------------------------------------------
        // GET: Client
        [Authorize(Policy = "Security")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.User.FindFirst("AirportId")?.Value;
            var clients = await _service.Get(userId);
            return View(clients);
        }

        // POST: Client
        [Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            var userId = HttpContext.User.FindFirst("AirportId")?.Value;
            await _service.Create(client, userId);
            return RedirectToAction(nameof(Get));
        }

        // PUT: Client/5
        [Authorize(Policy = "Security")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Client client)
        {
            if (id != client.ClientID)
                return BadRequest();

            try
            {
                await _service.Edit(id, client);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: Client/5
        [Authorize(Policy = "Security")]
        [HttpDelete("{id}")]
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
        // ------------------------------------------------------------   
    }
}
