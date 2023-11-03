using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AIRCOM.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBContext _context;
        private IConfiguration _config;
        public LoginController(BDContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(IFormCollection collection)
        {
            var user = await _context.Users.
                SingleOrDefaultAsync(x => x.Email == collection["Email"] && x.Pwd == collection["Pwd"]);

            if (user is null)
                return BadRequest(new { message = "Credenciales invalidas" });

            string jwtToken = GenerateToken(user);

            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserType", user.Role),
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
