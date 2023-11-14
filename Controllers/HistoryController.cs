using AIRCOM.Models;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        private readonly HistoryService _service;
        public HistoryController(HistoryService service)
        {
            _service = service;
        }

        // Security-Client -------------------------------------------
        // GET: HistoryController/GetHistoryShips/5
        [Authorize(Policy = "Security")]
        [Authorize(Policy = "Client")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryShips(string id)
        {
            var histories = await _service.GetHistoryShips(id);
            return View(histories);
        }
        // -----------------------------------------------------------   

        // Security --------------------------------------------------
        // GET: HistoryController/GetHistory
        [Authorize(Policy = "Security")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var histories = await _service.Get();
            return View(histories);
        }

        // POST: HistoryController/Create
        [Authorize(Policy = "Security")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] History history)
        {
            await _service.Create(history);
            return RedirectToAction(nameof(Get));
        }

        // PUT: HistoryController/Edit
        [Authorize(Policy = "Security")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] History history)
        {
            try
            {
                await _service.Edit(history);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: HistoryController/Delete
        [Authorize(Policy = "Security")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] History history)
        {
            try
            {
                await _service.Delete(history);
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return NotFound();
            }
        }
        // -----------------------------------------------------------   
    }
}
