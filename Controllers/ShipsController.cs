using AIRCOM.Models;
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
        // -----------------------------------------------------------   
    }
}
