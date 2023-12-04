using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Security")]
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
        public async Task<IActionResult> Index()
        {
            int page = 2;
            var ships = await _service.Get();
            return RedirectToAction("Index", "Security", new { ships = ships.ToList(), page = page });
        }

        // POST: Ships
        [HttpPost]
        public async Task<IActionResult> Create(ShipsDTO ship)
        {
            try
            {
                await _service.Create(ship);
                return RedirectToAction("Index", "Security", new { sucess = "Los cambios han sido realizados satisfactoriamente" });
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 6;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 6;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        // PUT: Ships/5
        //[Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Edit(ShipsDTO ship)
        {
            try
            {
                await _service.Edit(ship);
                return RedirectToAction("Index", "Security", new { sucess = "Los cambios han sido aplicados correctamente" });
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 7;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = "Error al insertar valores repetidos" }); 
            }
            catch (Exception e)
            {
                int lugar_del_error = 7;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        // DELETE: Ships/5
        //[Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Delete(string plate)
        {
            try
            {
                await _service.Delete(plate);
                return RedirectToAction("Index", "Security", new { sucess = "Los cambios han sido aplicados correctamente" });
            }
            catch (Exception e)
            {
                int lugar_del_error = 7;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        // -----------------------------------------------------------   
        /*
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
