using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AIRCOM.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginService _service;
        public HomeController(LoginService service)
        {
            _service = service;
        }

        public IActionResult Index(bool deleteCookie = false)
        {
            if (deleteCookie)
                HttpContext.Response.Cookies.Delete("UserData");
            ViewData["check"] = false;
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Register client)
        {
            try
            {
                var Token = await _service.Login(client);
                var userResponseJson = JsonConvert.SerializeObject(Token.Item1);
                HttpContext.Response.Cookies.Append("UserData", userResponseJson);

                switch (Token.Item2)
                {
                    case 0:
                        return RedirectToAction("Index", "CView");
                    case 1:
                        return RedirectToAction("Index", "Security");
                    case 2:
                        return RedirectToAction("Index", "Mechanic");
                    default:
                        return RedirectToAction("Index", "Direction");
                }
            }
            catch
            {
                ViewData["check"] = true;
                ViewData["error"] = "Credenciales incorrectos";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}