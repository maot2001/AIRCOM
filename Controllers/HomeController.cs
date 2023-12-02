using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AIRCOM.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public IActionResult Index()
        {
            ViewData["check"] = false;
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Register client)
        {
            try
            {
                var Token = await _service.Login(client);
                switch (Token.Item2)
                {
                    case 0:
                        return RedirectToAction(nameof(CViewController.Index), "CView");
                    case 1:
                        return RedirectToAction(nameof(SecurityController.Index), "Security", new { token = Token.Item1 });
                    case 2:
                        return RedirectToAction(nameof(MechanicController.Index), "Mechanic");
                    default:
                        return RedirectToAction(nameof(DirectionController.Index), "Direction");
                }
            }
            catch
            {
                ViewData["check"] = true;
                ViewData["error"] = "El usuario no existe";
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