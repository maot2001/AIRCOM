using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly HistoryService _service;
        public HistoryController(HistoryService service)
        {
            _service = service;
        }
        /*
        // Security-Client -------------------------------------------
        // GET: History/5
        [Authorize(Policy = "Security")]
        //[Authorize(Policy = "Client")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryShips(string plate)
        {
            var histories = await _service.Get(plate);
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
        */
        // POST: History
        //[Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Create(HistoryDTO history)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Create(history, userId);
                return RedirectToAction("Index", "Security", new { sucess = "Los cambios han sido aplicados correctamente" });
            }
            catch (DbUpdateException ex)
            {
                int lugar_del_error = 8;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = "Error al insertar valores repetidos" });
            }
            catch (Exception ex)
            {
                int lugar_del_error = 8;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = ex.Message });
            }
        }
            
        /*
        // PUT: History
        //[Authorize(Policy = "Security")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] HistoryDTO history)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Edit(history, userId);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: History
        //[Authorize(Policy = "Security")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] HistoryDTO history)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Delete(history, userId);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }
        // -----------------------------------------------------------
        */
        // Client ----------------------------------------------------
        // PUT: History/Include
        //[Authorize(Policy = "Client")]
        /*[HttpPut("Include")]
        public async Task<IActionResult> Include(HistoryDTO history)
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
        }*/
        // -----------------------------------------------------------
    }
}
