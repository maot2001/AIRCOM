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
    public class RepairController : Controller
    {
        private readonly RepairService _service;
        public RepairController(RepairService service)
        {
            _service = service;
        }

        // Admin -----------------------------------------------------
        // GET: Repair
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var repairs = await _service.Get();
            return View(repairs);
        }

        // POST: Repair
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RepairDTO repair)
        {
            try
            {
                await _service.Create(repair);
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

        // PUT: Repair
        [Authorize(Policy = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] RepairDTO repair)
        {
            try
            {
                await _service.Edit(repair);
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

        // DELETE: Repair/5
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
