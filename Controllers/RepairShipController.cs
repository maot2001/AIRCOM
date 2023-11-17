using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RepairShipController : Controller
    {
        private readonly RepairShipService _service;
        public RepairShipController(RepairShipService service)
        {
            _service = service;
        }

        // Security ------------------------------------------------------------
        // POST: RepairShip/5
        [HttpPost("{shipId}")]
        [Authorize(Policy = "Security")]
        public async Task<IActionResult> RequestRepair(string shipId, [FromBody] RepairInstallationDTO repair)
        {
            try
            {
                await _service.RequestRepair(shipId, repair);
                return RedirectToAction(nameof(GetRepairs));
            }
            catch
            {
                return NotFound();
            }
        }
        // ---------------------------------------------------------------------

        // Mechanic ------------------------------------------------------------
        // GET: RepairShip/GetRequest
        [HttpGet("GetRequest")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> GetRequest(string token = "")
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(0, userId);
            return View((rep, token));
        }

        // GET: RepairShip/GetProcess
        [HttpGet("GetProcess")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> GetProcess()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(1, userId);
            return View(rep);
        }

        // GET: RepairShip/GetFinish
        [HttpGet("GetFinish")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> GetFinish()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(2, userId);
            return View(rep);
        }

        // GET: RepairShip/GetRepairs
        [HttpGet("GetRepairs")]
        [Authorize(Policy = "Security")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> GetRepairs()
        {
            var userId = HttpContext.User.FindFirst("Airport")?.Value;
            var rep = await _service.Get(3, userId);
            return View(rep);
        }

        // PUT: RepairShip/ProcessRepair
        [HttpPut("ProcessRepair")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> ProcessRepair([FromBody] RepairShipDTO repair)
        {
            try
            {
                await _service.ProcessRepair(repair);
                return RedirectToAction(nameof(GetRequest));
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: RepairShip/FinishRepair
        [HttpPut("FinishRepair")]
        [Authorize(Policy = "Mechanic")]
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
        [HttpGet("ClientShip")]
        [Authorize(Policy = "Client")]
        public async Task<IActionResult> CSRepair()
        {
            var userId = int.Parse(HttpContext.User.FindFirst("Id")?.Value);
            var rep = await _service.Get(4, "", userId);
            return View(rep);
        }

        // PUT: Valorar Reparacion
        [HttpPut("Valorate")]
        [Authorize(Policy = "Client")]
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
        }
        // ---------------------------------------------------------------------
    }
}
