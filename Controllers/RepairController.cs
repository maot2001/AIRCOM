using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Get()
        {
            var repairs = await _service.Get();
            return View(repairs);
        }

        // POST: Repair
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] RepairDTO repair)
        {
            try
            {
                await _service.Create(repair);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: Repair
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit([FromBody] RepairDTO repair)
        {
            try
            {
                await _service.Edit(repair);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: Repair/5
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
