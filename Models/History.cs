using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    public class History
    {
        public DateTime? Date { get; set; }
        public string? OwnerRole { get; set; }
        public string? Plate { get; set; }
        [ForeignKey(nameof(Plate))]
        public virtual Ships? Ships { get; set; }
        public int AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }
    }
}
