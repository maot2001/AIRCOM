using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly RepairService _aux;
        public AdminController(RepairService aux)
        {
            _aux = aux;
        }

        public IActionResult Index()
        { 
            ViewData["page"] = 0;
            ViewData["lugar_del_error"] = 0;
            return View("Admin");
        }
        
        public IActionResult Init(int page = 1, int lugar_del_error = 0, string error = "")
        {
            ViewData["page"] = page;
            ViewData["lugar_del_error"] = lugar_del_error;
            ViewData["error"] = error;
            return View("Admin");
        }

        public IActionResult Comprobe(string pwd)
        {
            if (pwd == "1234")
            {
                return RedirectToAction(nameof(Init));
            }
            ViewData["page"] = 0;
            ViewData["lugar_del_error"] = 1;
            ViewData["error"] = "Contraseña inválida";
            return View("Admin");
        }

        private async Task<string> ObtenerContenidoDeLaSolicitud(HttpRequest request)
        {
            using (var reader = new StreamReader(request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
