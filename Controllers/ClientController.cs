using AIRCOM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly DBContext _context;
        public ClientController(DBContext context)
        {
            _context = context;
        }

        // Security --------------------------------------------------
        // GET: ClientController/Get
        [Authorize(Policy = "Security")]
        [HttpGet]
        public IActionResult Get()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }

        // POST: ClientController/Create
        [Authorize(Policy = "Security")]
        [HttpPost]
        public IActionResult Create([FromBody] Client client)
        {
            try
            {
                _context.Clients.Add(client);
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: ClientController/Edit/5
        [Authorize(Policy = "Security")]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Client client)
        {
            if (id != client.ClientID)
                return BadRequest();

            var clientBD = _context.Clients.Find(id);
            if (clientBD is null)
                return NotFound();

            try
            {
                clientBD.Name = client.Name;
                clientBD.Type = client.Type;
                clientBD.Nationality = client.Nationality;
                clientBD.Email = client.Email;
                clientBD.Pwd = client.Pwd;
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: ClientController/Delete/5
        [Authorize(Policy = "Security")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _context.Clients.Find(id);
            if (client is null)
                return NotFound();

            try
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return RedirectToAction(nameof(Get));
            }
            catch
            {
                return BadRequest();
            }
        }
        // ------------------------------------------------------------   
    }
}
