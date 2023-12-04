using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    [Authorize(Policy = "Security")]
    public class ClientController : Controller
    {
        private readonly string[] types = new string[] { "Seguridad", "Mecánico", "Dirección" };
        private readonly ClientService _service;
        public ClientController(ClientService service)
        {
            _service = service;
        }

        //[Authorize(Policy = "Client")]
        public async Task<IActionResult> Index()
        {
            ViewData["page"] = 3;
            ViewData["lugar_del_error"] = 0;
            ViewData["clients"] = await _service.Get("1");
            return View("Security");
        }
        /*{
            var clients = await _service.Get("1");
            return View(_mapper.Map<List<ClientDTO>>(clients));
        }*/


        [HttpPost]
        public async Task<IActionResult> Create(ClientDTO client)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("Airport")?.Value;
                await _service.Create(client, userId);
                return RedirectToAction("Index", "Security", new { sucess= "Los cambios han sido realizados satisfactoriamente" });
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 4;
                if (types.Contains(client.Type))
                    lugar_del_error = 5;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 4;
                if (types.Contains(client.Type))
                    lugar_del_error = 5;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClientDTO client)
        {
            try
            {
                await _service.Edit(client);
                return RedirectToAction("Index", "Security");
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 9;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 9;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                await _service.Delete(email);
                return RedirectToAction("Index", "Security");
            }
            catch (Exception e)
            {
                int lugar_del_error = 9;
                return RedirectToAction("Index", "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
    }
}
