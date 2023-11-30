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
            ViewData["p"] = 0;
            ViewData["a"] = 0;
            return View("Admin");
        }

        public IActionResult Init(string pwd)
        {
            if (pwd == "1234")
            {
                ViewData["p"] = 1;
                ViewData["a"] = 0;
                return View("Admin");
            }
            ViewData["p"] = 0;
            ViewData["a"] = 1;
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
