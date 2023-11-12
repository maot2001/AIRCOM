using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;
using AIRCOM.Models.DTO;

namespace AIRCOM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly DBContext _context;
        private IConfiguration _config;
        public LoginController(DBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // POST: LoginController/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Register register)
        {
            var user = await _context.Clients.SingleOrDefaultAsync(x => x.Email == register.Email && x.Pwd == register.Pwd); ;

            if (user is null)
                return BadRequest(new { message = "Credenciales invalidas" });

            string jwtToken = GenerateToken(user, register.worker);

            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(Client user, bool worker)
        {
            Client client = null;
            client.Email = user.Email;
            if (worker)
                client.Type = user.Type;
            else client.Type = "Client";

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserType", user.Type),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
