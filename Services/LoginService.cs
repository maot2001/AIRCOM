using AIRCOM.Models;
using AIRCOM.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AIRCOM.Services
{
    public class LoginService
    {
        private IConfiguration _config;
        private readonly DBContext _context;
        public LoginService(DBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> Login(Register register)
        {
            bool worker = register.worker > 0;
            IUser user;

            if (worker)
                user = await _context.Workers.SingleOrDefaultAsync(x => x.Email == register.Email && x.Pwd == register.Pwd);
            else
            {
                user = await _context.Clients.SingleOrDefaultAsync(x => x.Email == register.Email && x.Pwd == register.Pwd);
            }

            if (user is null)
                throw new Exception();
            if (!worker)
                user.AirportId = 0;

            string jwtToken = GenerateToken(user);

            return jwtToken;
        }

        //------------------------------------------------------------------
        private string GenerateToken(IUser user)
        {
            var claims = new[]
            {
                new Claim("Airport", user.AirportId.ToString()),
                new Claim("UserType", user.Type)
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
