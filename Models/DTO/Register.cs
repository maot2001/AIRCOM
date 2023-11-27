using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models.DTO
{
    public class Register
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Pwd { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}
