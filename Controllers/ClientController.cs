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
        public async Task<IActionResult> Get(string token = "")
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var clients = await _service.Get(userId);
            return View((clients, token));
        }

        // POST: Client
        [Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDTO client)
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            await _service.Create(client, userId);
            return RedirectToAction(nameof(Get));
        }

        // PUT: Client/5
        [Authorize(Policy = "Security")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ClientDTO client)
        {
            try
            {
                await _service.Edit(client);
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
        public async Task<IActionResult> Delete([FromBody] ClientDTO client)
        {
            try
            {
                await _service.Delete(client);
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
