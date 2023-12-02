using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class ClientController : Controller
    {
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


        //[Authorize(Policy = "Client")]
        [HttpPost]
        public async Task<IActionResult> Create(ClientDTO client)
        {
            try
            {
                await _service.Create(client, "1");
                return RedirectToAction(nameof(SecurityController.Index), "Security");
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 4;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction(nameof(SecurityController.Index), "Security", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 4;
                return RedirectToAction(nameof(SecurityController.Index), "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClientDTO client)
        {
            try
            {
                await _service.Edit(client);
                return RedirectToAction(nameof(SecurityController.Index), "Security");
            }
            catch (DbUpdateException e)
            {
                int lugar_del_error = 9;
                string error = "Error al insertar valores repetidos";
                return RedirectToAction(nameof(SecurityController.Index), "Security", new { lugar_del_error = lugar_del_error, error = error });
            }
            catch (Exception e)
            {
                int lugar_del_error = 9;
                return RedirectToAction(nameof(SecurityController.Index), "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                await _service.Delete(email);
                return RedirectToAction(nameof(SecurityController.Index), "Security");
            }
            catch (Exception e)
            {
                int lugar_del_error = 9;
                return RedirectToAction(nameof(AdminController.Init), "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
    }
}
