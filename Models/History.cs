using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(ID), IsUnique = true)]
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public string? OwnerRole { get; set; }
        public string Plate { get; set; }
        [ForeignKey(nameof(Plate))]
        public virtual Ships Ships { get; set; }
        public int? ArrivalID { get; set; }
        [ForeignKey(nameof(ArrivalID))]
        public virtual Airport? ArrivalAirport { get; set; }
        public int? ExitID { get; set; }
        [ForeignKey(nameof(ExitID))]
        public virtual Airport? ExitAirport { get; set; }
    }
}
