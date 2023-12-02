using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class RepairShipController : Controller
    {
        private readonly RepairShipService _service;
        public RepairShipController(RepairShipService service)
        {
            _service = service;
        }

        // Security ------------------------------------------------------------
        // POST: RepairShip/5
        //[Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> RequestRepair(string plate, RepairInstallationDTO repair)
        {
            try
            {
                await _service.RequestRepair(plate, repair);
                return RedirectToAction(nameof(SecurityController.Index), "Security");
            }
            catch (Exception e)
            {
                int lugar_del_error = 7;
                return RedirectToAction(nameof(SecurityController.Index), "Security", new { lugar_del_error = lugar_del_error, error = e.Message });
            }
        }
        // ---------------------------------------------------------------------

        // Mechanic ------------------------------------------------------------
        // GET: RepairShip/GetRequest
        //[Authorize(Policy = "Mechanic")]
        [HttpGet("GetRequest")]
        public async Task<IActionResult> GetRequest(string? token = null)
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(0, userId);
            return View((rep, token));
        }

        // GET: RepairShip/GetProcess
        //[Authorize(Policy = "Mechanic")]
        [HttpGet("GetProcess")]
        public async Task<IActionResult> GetProcess()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(1, userId);
            return View(rep);
        }

        // GET: RepairShip/GetFinish
        //[Authorize(Policy = "Mechanic")]
        [HttpGet("GetFinish")]
        public async Task<IActionResult> GetFinish()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(2, userId);
            return View(rep);
        }

        // GET: RepairShip/GetRepairs
        //[Authorize(Policy = "Security")]
        //[Authorize(Policy = "Mechanic")]
        [HttpGet("GetRepairs")]
        public async Task<IActionResult> GetRepairs()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(3, userId);
            return View(rep);
        }

        // PUT: RepairShip/ProcessRepair
        //[Authorize(Policy = "Mechanic")]
        [HttpPost]
        public async Task<IActionResult> ProcessRepair(RepairShipDTO repair)
        {
            try
            {
                await _service.ProcessRepair(repair);
                return RedirectToAction(nameof(MechanicController.Index), "Mechanic");
            }
            catch
            {
                return RedirectToAction(nameof(MechanicController.Index), "Mechanic");
            }
        }

        // PUT: RepairShip/FinishRepair
        //[Authorize(Policy = "Mechanic")]
        [HttpPost]
        public async Task<IActionResult> FinishRepair([FromBody] RepairShipDTO repair)
        {
            try
            {
                await _service.FinishRepair(repair);
                return RedirectToAction(nameof(GetProcess));
            }
            catch
            {
                return NotFound();
            }
        }
        // ---------------------------------------------------------------------

        // Client --------------------------------------------------------------
        // GET: RepairShip/ClientShip
        //[Authorize(Policy = "Client")]
        [HttpGet("ClientShip")]
        public async Task<IActionResult> CSRepair()
        {
            var userId = int.Parse(HttpContext.User.FindFirst("Id")?.Value);
            var rep = await _service.Get(4, "", userId);
            return View(rep);
        }

        // PUT: Valorar Reparacion
        //[Authorize(Policy = "Client")]
        /*[HttpPut("Valorate")]
        public async Task<IActionResult> Valorate([FromBody] RepairShipDTO repair)
        {
            try
            {
                await _service.Valorate(repair);
                return RedirectToAction(nameof(ShipsController.ClientShips), "Ships");
            }
            catch
            {
                return NotFound();
            }
        }*/
        // ---------------------------------------------------------------------
    }
}
