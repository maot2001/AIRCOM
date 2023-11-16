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
                return Ok(new { token = jwtToken });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
