using AIRCOM.Models;
using AIRCOM.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShipsController : Controller
    {
        private readonly DBContext _context;
        public ShipsController(DBContext context)
        {
            _context = context;
        }

        // Security --------------------------------------------------
        // GET: ShipsController/Get
        [Authorize(Policy = "Security")]
        [HttpGet]
        public IActionResult Get()
        {
            var ships = _context.Ships.ToList();
            return View(ships);
        }

        // POST: ShipsController/Create
        [Authorize(Policy = "Security")]
        [HttpPost]
        public IActionResult Create([FromBody] Ships ship)
        {
            try
            {
                _context.Ships.Add(ship);
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: ShipsController/Edit/5
        [Authorize(Policy = "Security")]
        [HttpPut("{id}")]
        public IActionResult Edit(string id, [FromBody] Ships ship)
        {
            if (id != ship.Plate)
                return BadRequest();

            var shipBD = _context.Ships.Find(id);
            if (shipBD is null)
                return NotFound();

            try
            {
                shipBD.Model = ship.Model;
                shipBD.Capacity = ship.Capacity;
                shipBD.Crew = ship.Crew;
                shipBD.ClientID = ship.ClientID;
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: ShipsController/Delete
        [Authorize(Policy = "Security")]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var ship = _context.Ships.Find(id);
            if (ship is null)
                return NotFound();

            try
            {
                _context.Ships.Remove(ship);
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: ShipsController/RequestRepair/5
        [HttpPost("{shipId}")]
        [Authorize(Policy = "Security")]
        public IActionResult RequestRepair(string shipId, [FromBody] RepairInstallationDTO repairId)
        {
            var ship = _context.Ships.Find(shipId);
            var repair = _context.RepairInstallations.SingleOrDefault(r => 
            r.InstallationID == repairId.InstallationID && r.AirportID == repairId.AirportID && r.RepairID == repairId.RepairID);

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
                    State = repairId.State,
                    Price = repair.Price
                };

                _context.RepairShips.Add(RS);
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }

            return NotFound();
        }
        // -----------------------------------------------------------   
    }
}
