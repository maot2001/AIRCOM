using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;
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

        // POST: LoginController/Login
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
