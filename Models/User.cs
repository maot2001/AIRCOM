using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Pwd { get; set; }
        public string? Role { get; set; }

    }
}
