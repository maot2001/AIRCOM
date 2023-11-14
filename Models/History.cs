using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(Plate),IsUnique = true)]
    [Index(nameof(Date),IsUnique = true)]
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? OwnerRole { get; set; }
        public int? Plate { get; set; }
        [ForeignKey(nameof(Plate))]
        public virtual Ships? Ships { get; set; }
        public int AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }
    }
}
