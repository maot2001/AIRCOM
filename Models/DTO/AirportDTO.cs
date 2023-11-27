using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models.DTO
{
    public class AirportDTO
    {
        public int? AirportID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Coordinates { get; set; }
        [Required]
        public string Direction { get; set; }
        public ClientDTO Security { get; set; }
    }
}
