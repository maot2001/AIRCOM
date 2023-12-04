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
//using Rotativa.AspNetCore;

namespace AIRCOM.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration _config;
        private readonly RepairService _aux;
        private readonly ConsultService _aux2;
        public AdminController(IConfiguration config, RepairService aux , ConsultService aux2)
        {
            _config = config;
            _aux = aux;
            _aux2 = aux2;
        }

        public IActionResult Index()
        { 
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
        public async Task<IActionResult> Init(int lugar_del_error = 0, string error = "",string sucess="")
        {
            ViewData["sucess"] = sucess;
            ViewData["lugar_del_error"] = lugar_del_error;
            ViewData["error"] = error;
            var repairs = await _aux.Get();
            ViewData["repairs"] = new SelectList(repairs, "RepairID", "Name");
            return View("Administrar");
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ChangePage(string page)
        {
            var a = await _aux2.GetPoint1();
            ViewData["Nombre_posición"] = a;
            ViewData["cant_reps"] = await _aux2.GetPoint2();
            ViewData["capitán_Aero"] = await _aux2.GetPoint3();
            ViewData["punto4"] = await _aux2.GetPoint4();
            ViewData["punto5"] = await _aux2.GetPoint5();
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
