using AIRCOM.Models.DTO;
using AIRCOM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIRCOM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ConsultController : Controller
    {
        private readonly ConsultService _service;  //done
        public ConsultController(ConsultService service)
        {
            _service = service;
        }




    }
}
