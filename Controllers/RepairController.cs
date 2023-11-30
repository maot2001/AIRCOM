using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class RepairController : Controller
    {
        private readonly RepairService _service;
        public RepairController(RepairService service)
        {
            _service = service;
        }

        // Admin -----------------------------------------------------
        // GET: Repair
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Get()
        {
            var repairs = await _service.Get();
            return View(repairs);
        }

        // POST: Repair
        //[Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(RepairDTO repair)
        {
            ViewData["p"] = 1;
            try
            {
                await _service.Create(repair);
                ViewData["a"] = 0;
                return View("Admin");
            }
            catch (Exception e)
            {
                ViewData["a"] = 2;
                ViewData["error"] = e.Message;
                return View("Admin");
            }
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
        //[Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string name, bool cascada)
        {
            ViewData["p"] = 1;
            try
            {
                await _service.Delete(name, cascada);
                ViewData["a"] = 0;
                return View("Admin");
            }
            catch (Exception e)
            {
                ViewData["a"] = 5;
                ViewData["error"] = e.Message;
                return View("Admin");
            }
        }
        // ---------------------------------------------------------------------
    }
}
