﻿using AIRCOM.Models;
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
                string jwtToken = await _service.Login(client);
                return RedirectToAction("Index", "Client");
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