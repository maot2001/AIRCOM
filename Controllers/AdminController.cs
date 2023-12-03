using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AIRCOM.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration _config;
        private readonly RepairService _aux;
        public AdminController(IConfiguration config, RepairService aux)
        {
            _config = config;
            _aux = aux;
        }

        public IActionResult Index()
        { 
            ViewData["page"] = 0;
            ViewData["lugar_del_error"] = 0;
            return View("Admin");
        }
        public IActionResult Comprobe(string pwd)
        {
            if (pwd == "1234")
            {
                string token = GenerateToken();
                var userResponseJson = JsonConvert.SerializeObject(token);
                HttpContext.Response.Cookies.Append("UserData", userResponseJson);
                return RedirectToAction(nameof(Init));
            }
            ViewData["lugar_del_error"] = 1;
            ViewData["error"] = "Contraseña inválida";
            return View("Admin");
        }
        
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Init(int lugar_del_error = 0, string error = "")
        {
            ViewData["lugar_del_error"] = lugar_del_error;
            ViewData["error"] = error;
            //var repairs = await _aux.Get();
            //ViewData["repairs"] = new SelectList(repairs, "RepairID", "Name");
            return View("Administrar");
        }

        [Authorize(Policy = "Admin")]
        public IActionResult ChangePage(string page)
        {
            return View("Estadisticas");
        }

        private string GenerateToken()
        {
            var claims = new[]
            {
                new Claim("UserType", "Administrador")
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
