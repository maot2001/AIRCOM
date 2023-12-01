using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models.DTO
{
    public class ClientDTO
    {
        public int? ClientID { get; set; }
        public int? AirportID { get; set; }
        public string Name { get; set; }
        public string CI { get; set; }
        public string Type { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string? Pwd { get; set; }
        public string? Rol { get; set; }

    }
}
