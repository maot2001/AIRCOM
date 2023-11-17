using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly ServicesService _service;
        public ServicesController(ServicesService service)
        {
            _service = service;
        }

        // Admin -----------------------------------------------------
        // GET: Services
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Get()
        {
            var service = await _service.Get();
            return View(service);
        }

        // POST: Services
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] ServiceDTO service)
        {
            try
            {
                await _service.Create(service);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: Services
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit([FromBody] ServiceDTO service)
        {
            try
            {
                await _service.Edit(service);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: Services/5
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
