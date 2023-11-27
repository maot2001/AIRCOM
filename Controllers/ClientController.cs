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
            => View(await _service.Get("1"));
        /*{
            var clients = await _service.Get("1");
            return View(_mapper.Map<List<ClientDTO>>(clients));
        }*/

        //[Authorize(Policy = "Client")]
        public IActionResult Create()
            => View();

        //[Authorize(Policy = "Client")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientDTO client)
        {
            try
            {
                await _service.Create(client, "1");
                return RedirectToAction(nameof(Index));
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

        public IActionResult Edit(int id)
            => View(new ClientDTO { ClientID = id});

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientDTO client)
        {
            try
            {
                await _service.Edit(client);
                return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> Delete(int id, string name)
        {
            try
            {
                await _service.Delete(new ClientDTO { ClientID = id, Type = name });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
