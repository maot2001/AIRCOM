using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        
        public async Task<IActionResult> Init(int lugar_del_error = 0, string error = "",string sucess="")
        {
            ViewData["sucess"] = sucess;
            ViewData["lugar_del_error"] = lugar_del_error;
            ViewData["error"] = error;
            //var repairs = await _aux.Get();
            //ViewData["repairs"] = new SelectList(repairs, "RepairID", "Name");
            return View("Administrar");
        }

        public IActionResult Comprobe(string pwd)
        {
            if (pwd == "1234")
            {
                return RedirectToAction(nameof(Init));
            }
            ViewData["lugar_del_error"] = 1;
            ViewData["error"] = "Contraseña inválida";
            return View("Admin");
        }

        public IActionResult ChangePage(string page)
        {
            return View("Estadisticas");
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
