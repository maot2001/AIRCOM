using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(ID), IsUnique = true)]
    public class RepairShip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Init { get; set; }
        public DateTime? Finish { get; set; }
        public string State { get; set; }
        public int Time { get; set; }
        public float Price { get; set; }
        public int? Stars { get; set; } = 0;
        public string? Comment { get; set; }
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Installation Installation { get; set; }
        public int AirportID { get; set; }
        /*[ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }*/
        public int RepairID { get; set; }
        [ForeignKey(nameof(RepairID))]
        public virtual Repair Repair { get; set; }
        public string Plate { get; set; }
        [ForeignKey(nameof(Plate))]
        public virtual Ships Ships { get; set; }
    }
}
