using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(RepairID),IsUnique =true)]
    [Index(nameof(Name),IsUnique =true)]
    public class Repair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RepairID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public virtual ICollection<RepairShip>? RepairShip { get; set; }
        public virtual ICollection<RepairInstallation>? RepairInstallations { get; set; }
    }
}
