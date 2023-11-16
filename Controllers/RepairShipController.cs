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
        public async Task<IActionResult> RequestRepair(string shipId, [FromBody] RepairInstallationDTO repairId)
        {
            try
            {
                await _service.RequestRepair(shipId, repairId);
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
        public async Task<IActionResult> GetRequest()
        {
            var rep = await _service.Get(0);
            return View(rep);
        }

        // GET: RepairShip/GetProcess
        [HttpGet("GetProcess")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> GetProcess()
        {
            var rep = await _service.Get(1);
            return View(rep);
        }

        // GET: RepairShip/GetFinish
        [HttpGet("GetFinish")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> GetFinish()
        {
            var rep = await _service.Get(2);
            return View(rep);
        }

        // GET: RepairShip/GetRepairs
        [HttpGet("GetRepairs")]
        [Authorize(Policy = "Security")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> GetRepairs()
        {
            var rep = await _service.Get(3);
            return View(rep);
        }

        // PUT: RepairShip/ProcessRepair
        [HttpPut("ProcessRepair")]
        [Authorize(Policy = "Mechanic")]
        public async Task<IActionResult> ProcessRepair([FromBody] RepairShipDTO repairId)
        {
            try
            {
                await _service.ProcessRepair(repairId);
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
        public async Task<IActionResult> FinishRepair([FromBody] RepairShipDTO repairId)
        {
            try
            {
                await _service.FinishRepair(repairId);
                return RedirectToAction(nameof(GetProcess));
            }
            catch
            {
                return NotFound();
            }
        }
        // ---------------------------------------------------------------------

        // Client --------------------------------------------------------------
        // GET Reparaciones en Naves Propias
        // ---------------------------------------------------------------------
    }
}
