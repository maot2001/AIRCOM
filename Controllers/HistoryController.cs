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
            try
            {
                _context.Histories.Add(history);
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: HistoryController/Edit
        [Authorize(Policy = "Security")]
        [HttpPut]
        public IActionResult Edit([FromBody] (int, string, DateTime) id, [FromBody] History history)
        {
            if (id != (history.AirportID, history.Plate, history.Date))
                return BadRequest();

            var historyBD = _context.Histories.Find(id);
            if (historyBD is null)
                return NotFound();

            try
            {
                historyBD.Date = history.Date;
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
        public IActionResult Delete([FromBody] (int, string, DateTime) id)
        {
            var history = _context.Histories.Find(id);
            if (history is null)
                return NotFound();

            try
            {
                _context.Histories.Remove(history);
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
