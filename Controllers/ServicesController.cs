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
    public class ServicesController : Controller
    {
        private readonly ServicesService _service;
        public ServicesController(ServicesService service)
        {
            _service = service;
        }

        // Admin -----------------------------------------------------
        // GET: Services
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var service = await _service.Get();
            return View(service);
        }

        // POST: Services
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDTO service)
        {
            try
            {
                await _service.Create(service);
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

        // PUT: Services
        [Authorize(Policy = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ServiceDTO service)
        {
            try
            {
                await _service.Edit(service);
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

        // DELETE: Services/5
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
