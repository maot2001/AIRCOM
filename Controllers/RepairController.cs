using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Admin")]
    public class RepairController : Controller
    {
        private readonly RepairService _service;
        public RepairController(RepairService service)
        {
            _service = service;
        }

        // Admin -----------------------------------------------------
        // GET: Repair
        public async Task<IActionResult> Get()
        {
            var repairs = await _service.Get();
            return View(repairs);
        }

        // POST: Repair
        [HttpPost]
        public async Task<IActionResult> Create(RepairDTO repair)
        {
            await _service.Create(repair);
            return RedirectToAction("Init", "Admin");
        }
        /*
        // PUT: Repair
        //[Authorize(Policy = "Admin")]
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
        }*/

        // DELETE: Repair/5
        [HttpPost]
        public async Task<IActionResult> Delete(string name, bool cascada)
        {
            try
            {
                await _service.Delete(name, cascada);
                return RedirectToAction("Init", "Admin");
            }
            catch (Exception e)
            {
                int lugar_del_error = 5;
                return RedirectToAction("Init", "Admin", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        // ---------------------------------------------------------------------
    }
}
