using AIRCOM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        private readonly DBContext _context;
        public HistoryController(DBContext context)
        {
            _context = context;
        }

        // Security-Client -------------------------------------------
        // GET: HistoryController/GetHistoryShips/5
        [Authorize(Policy = "Security")]
        [Authorize(Policy = "Client")]
        [HttpGet("{id}")]
        public IActionResult GetHistoryShips(string id)
        {
            var histories = _context.Histories.Where(h => h.Plate == id).ToList();
            return View(histories);
        }
        // -----------------------------------------------------------   

        // Security --------------------------------------------------
        // GET: HistoryController/GetHistory
        [Authorize(Policy = "Security")]
        [HttpGet]
        public IActionResult Get()
        {
            var histories = _context.Histories.ToList();
            return View(histories);
        }

        // POST: HistoryController/Create
        [Authorize(Policy = "Security")]
        [HttpPost]
        public IActionResult Create([FromBody] History history)
        {
            var ship = _context.Ships.Find(history.Plate);
            var airport = _context.Airports.Find(history.AirportID);

            if (ship is null || airport is null)
                return BadRequest();

                _context.Histories.Add(history);
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
        }

        // PUT: HistoryController/Edit
        [Authorize(Policy = "Security")]
        [HttpPut]
        public IActionResult Edit([FromBody] History history)
        {
            var historyBD = _context.Histories.SingleOrDefault(h =>
            h.Plate == history.Plate && h.AirportID == history.AirportID && h.Date == history.Date);
            if (historyBD is null)
                return NotFound();

            try
            {
                historyBD.OwnerRole = history.OwnerRole;
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: HistoryController/Delete
        [Authorize(Policy = "Security")]
        [HttpDelete]
        public IActionResult Delete([FromBody] History history)
        {
            var historyBD = _context.Histories.SingleOrDefault(h =>
            h.Plate == history.Plate && h.AirportID == history.AirportID && h.Date == history.Date);
            if (historyBD is null)
                return NotFound();

            try
            {
                _context.Histories.Remove(historyBD);
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
