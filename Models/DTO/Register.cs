using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models.DTO
{
    public class Register
    {
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Rol { get; set; }
    }
}
