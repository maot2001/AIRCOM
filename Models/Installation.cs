using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    [Index(nameof(ID), IsUnique = true)]
    public class Installation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int InstallationID { get; set; }
        public string Name { get; set; }
        public string Ubication { get; set; }
        public string Type { get; set; }
        public int AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport Airport { get; set; }
        public bool Active { get; set; } = true;
        public virtual ICollection<ServicesInstallation>? ServicesInstallations { get; set; }
        //public virtual ICollection<on_site> on_sites { get; set; }
        public virtual ICollection<RepairInstallation>? RepairInstallations { get; set; }
        //public virtual ICollection<RepairShip> RepairShip { get; set; }
    }
}
