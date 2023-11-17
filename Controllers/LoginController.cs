using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AIRCOM.Models.DTO;
using AIRCOM.Services;

namespace AIRCOM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginService _service;
        public LoginController(LoginService service)
        {
            _service = service;
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Register register)
        {
            try
            {
                string jwtToken = await _service.Login(register);
                switch (register.worker)
                {
                    case 0:
                        return RedirectToAction(nameof(ShipsController.ClientShips), "Ships", jwtToken);
                    case 1:
                        return RedirectToAction(nameof(ClientController.Get), "Client", jwtToken);
                    case 2:
                        return RedirectToAction(nameof(RepairShipController.GetRequest), "RepairShip", jwtToken);
                    case 3:
                        return RedirectToAction(nameof(InstallationController.Get), "Installation", jwtToken);
                    default:
                        return RedirectToAction(nameof(AirportController.Get), "Airport", jwtToken);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
