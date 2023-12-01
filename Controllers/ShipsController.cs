using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class ShipsController : Controller
    {
        private readonly ShipsService _service;
        public ShipsController(ShipsService service)
        {
            _service = service;
        }

        // Security --------------------------------------------------
        // GET: Ships
        //[Authorize(Policy = "Security")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["p"] = 2;
            ViewData["a"] = 0;
            ViewData["ships"] = await _service.Get();
            return View("Security");
        }

        // POST: Ships
        //[Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Create(ShipsDTO ship)
        {
            ViewData["p"] = 4;
            try
            {
                ViewData["a"] = 0;
                await _service.Create(ship);
                return View("Security");
            }
            catch (DbUpdateException e)
            {
                ViewData["a"] = 6;
                ViewData["error"] = "Error al insertar valores repetidos";
                return View("Security");
            }
            catch (Exception e)
            {
                ViewData["a"] = 6;
                ViewData["error"] = e.Message;
                return View("Security");
            }
        }

        // PUT: Ships/5
        //[Authorize(Policy = "Security")]
        /*[HttpPut]
        public async Task<IActionResult> Edit([FromBody] ShipsDTO ship)
        {
            try
            {
                await _service.Edit(ship);
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

        // DELETE: Ships/5
        //[Authorize(Policy = "Security")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
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
        // -----------------------------------------------------------   

        // Client ----------------------------------------------------
        // GET: Ships/Client
        //[Authorize(Policy = "Client")]
        [HttpGet("Client")]
        public async Task<IActionResult> ClientShips(string? token = null)
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            var ships = await _service.Get(userId);
            return View((ships, token));
        }
        // -----------------------------------------------------------   
        */
    }
}
