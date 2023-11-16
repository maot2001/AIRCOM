using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class RepairInstallation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Installation? Installation { get; set; }
        public int? AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }
        public int? RepairID { get; set; }
        [ForeignKey(nameof(RepairID))]
        public virtual Repair? Repair { get; set; }
        public virtual ICollection<RepairShip> RepairShips { get; set; }
    }
}
