using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models.DTO
{
    public class ClientDTO
    {
        public int? ClientID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CI { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Nationality { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string? Pwd { get; set; }

    }
}
