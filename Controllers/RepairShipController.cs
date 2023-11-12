using AIRCOM.Models;
using AIRCOM.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RepairShipController : Controller
    {
        private readonly DBContext _context;
        public RepairShipController(DBContext context)
        {
            _context = context;
        }

        // Mechanic ------------------------------------------------------------
        [HttpGet("GetRequest")]
        public IActionResult GetRequest()
        {
            return View(Get(0));
        }

        [HttpGet("GetProcess")]
        public IActionResult GetProcess()
        {
            return View(Get(1));
        }

        [HttpGet("GetFinish")]
        public IActionResult GetFinish()
        {
            return View(Get(2));
        }

        [HttpGet("GetRepairs")]
        public IActionResult GetRepairs()
        {
            return View(Get(3));
        }


        // PUT: RepairShipController/ProcessRepair
        [HttpPut("ProcessRepair")]
        [Authorize(Policy = "Mechanic")]
        public IActionResult ProcessRepair([FromBody] RepairShipDTO repairId)
        {
            if (repairId.newTime == default(DateTime))
                repairId.newTime = DateTime.Now;

            var shipRepair = _context.RepairShips.SingleOrDefault(r =>
            r.InstallationID == repairId.InstallationID && r.AirportID == repairId.AirportID && r.RepairID == repairId.RepairID &&
            r.Plate == repairId.Plate && r.Init == repairId.Init);
            if (shipRepair is null)
                return NotFound();

            try
            {
                shipRepair.Init = (DateTime) repairId.newTime;
                _context.SaveChanges();
                return RedirectToAction(nameof(GetRequest));
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: RepairShipController/FinishRepair
        [HttpPut("FinishRepair")]
        [Authorize(Policy = "Mechanic")]
        public IActionResult FinishRepair([FromBody] RepairShipDTO repairId)
        {
            if (repairId.newTime == default(DateTime))
                repairId.newTime = DateTime.Now;

            var shipRepair = _context.RepairShips.SingleOrDefault(r =>
            r.InstallationID == repairId.InstallationID && r.AirportID == repairId.AirportID && r.RepairID == repairId.RepairID &&
            r.Plate == repairId.Plate && r.Init == repairId.Init);
            if (shipRepair is null)
                return NotFound();

            try
            {
                TimeSpan time = (DateTime) repairId.newTime - shipRepair.Init;
                int cost = shipRepair.Time - (int)time.TotalHours;
                if (cost > 0)
                    shipRepair.Price *= (1 + cost / 100);

                shipRepair.Finish = (DateTime) repairId.newTime;
                _context.SaveChanges();
                return RedirectToAction(nameof(GetProcess));
            }
            catch
            {
                return BadRequest();
            }
        }
        // ---------------------------------------------------------------------
        private IEnumerable<RepairShip> Get(int type)
        {
            List<RepairShip> repairs;
            switch (type)
            {
                case 0:
                    repairs = _context.RepairShips.Where(SR => SR.Init == default(DateTime)).ToList();
                    break;
                case 1:
                    repairs = _context.RepairShips.Where(SR => SR.Finish == default(DateTime) && SR.Init != default(DateTime)).ToList();
                    break;
                case 2:
                    repairs = _context.RepairShips.Where(SR => SR.Finish != default(DateTime)).ToList();
                    break;
                default:
                    repairs = _context.RepairShips.ToList();
                    break;
            }

            return repairs;
        }

    }
}
