using AIRCOM.Models;
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

        // Security ------------------------------------------------------------
        // POST: RepairShipController/RequestRepair/5
        [HttpPost("{shipId}")]
        [Authorize(Policy = "Security")]
        public IActionResult RequestRepair(string shipId, [FromBody] (int, int, int) repairId, [FromBody] string state)
        {
            var ship = _context.Ships.Find(shipId);
            var repair = _context.RepairInstallations.Find(repairId);

            if (ship is not null && repair is not null)
            {
                var RS = new RepairShip
                {
                    Plate = shipId,
                    RepairID = repair.RepairID,
                    InstallationID = repair.InstallationID,
                    AirportID = repair.AirportID,
                    Ships = ship,
                    Repair = repair.Repair,
                    Installation = repair.Installation,
                    Airport = repair.Airport,
                    State = state,
                    Price = repair.Price
                };

                _context.RepairShips.Add(RS);
                _context.SaveChanges();
                return View();
            }

            return NotFound();
        }
        // ---------------------------------------------------------------------

        // Mechanic ------------------------------------------------------------
        // GET: RepairShipController/Get/5
        [HttpGet("{type}")]
        [Authorize(Policy = "Mechanic")]
        public IActionResult Get(int type)
        {
            List<RepairShip> repairs;
            switch (type)
            {
                case 0:
                    repairs = _context.RepairShips.Where(SR => SR.Init == default(DateTime)).ToList();
                    break;
                case 1:
                    repairs = _context.RepairShips.Where(SR => SR.Finish == default(DateTime)).ToList();
                    break;
                case 2:
                    repairs = _context.RepairShips.Where(SR => SR.Finish != default(DateTime)).ToList();
                    break;
                default:
                    repairs = _context.RepairShips.ToList();
                    break;
            }

            return View((repairs, type));
        }

        // PUT: RepairShipController/ProcessRepair
        [HttpPut]
        [Authorize(Policy = "Mechanic")]
        public IActionResult ProcessRepair([FromBody] (int, int, int, string, DateTime) id, [FromBody] DateTime init = default(DateTime))
        {
            if (init == default(DateTime))
                init = DateTime.Now;

            var shipRepair = _context.RepairShips.Find(id);
            if (shipRepair is null)
                return NotFound();

            try
            {
                shipRepair.Init = init;
                _context.SaveChanges();
                return Get(0);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: RepairShipController/FinishRepair
        [HttpPut]
        [Authorize(Policy = "Mechanic")]
        public IActionResult FinishRepair([FromBody] (int, int, int, string, DateTime) id, [FromBody] DateTime finish = default(DateTime))
        {
            if (finish == default(DateTime))
                finish = DateTime.Now;

            var shipRepair = _context.RepairShips.Find(id);
            if (shipRepair is null)
                return NotFound();

            try
            {
                TimeSpan time = finish - shipRepair.Init;
                int cost = shipRepair.Time - (int)time.TotalHours;
                if (cost > 0)
                    shipRepair.Price *= (1 + cost / 100);

                shipRepair.Finish = finish;
                _context.SaveChanges();
                return Get(1);
            }
            catch
            {
                return BadRequest();
            }
        }
        // ---------------------------------------------------------------------
    }
}
