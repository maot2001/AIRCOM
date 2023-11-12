using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;
using AIRCOM.Models.Interfaces;

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
        public async Task<IActionResult> Login([FromBody] string Email, [FromBody] string Pwd, [FromBody] bool worker)
        {
            IUser user;
            if (worker)
                user = await _context.Workers.SingleOrDefaultAsync(x => x.Email == Email && x.Pwd == Pwd);
            else    
                user = await _context.Clients.SingleOrDefaultAsync(x => x.Email == Email && x.Pwd == Pwd);

            if (user is null)
                return BadRequest(new { message = "Credenciales invalidas" });

            string jwtToken = GenerateToken(user);

            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(IUser user)
        {
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
