using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly Dictionary<string, int> types = new () { 
            { "Seguridad", 1 },  { "Mecánico", 2 }, { "Dirección", 3 }, { "Administrador", 4 } };
        public LoginService(DBContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<(string, int)> Login(Register register)
        {
            string jwtToken;
            int val = 0;

            if (types.ContainsKey(register.Rol))
            {
                val = types[register.Rol];
                var worker = await _context.Workers.SingleOrDefaultAsync(x => x.Email == register.Email && x.Pwd == register.Pwd);
                if (worker is null)
                    throw new Exception();
                jwtToken = GenerateToken(worker.AirportID.ToString(), worker.WorkerID.ToString(), worker.Type);
            }
            else
            {
                var client = await _context.Clients.SingleOrDefaultAsync(x => x.Email == register.Email && x.Pwd == register.Pwd);
                if (client is null)
                    throw new Exception();
                jwtToken = GenerateToken("0", client.ClientID.ToString(), "Client");
            }

            return (jwtToken, val);
        }

        //------------------------------------------------------------------
        private string GenerateToken(string AirportId, string Id, string Type)
        {
            var claims = new[]
            {
                new Claim("Airport", AirportId),
                new Claim("Id", Id),
                new Claim("UserType", Type)
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
