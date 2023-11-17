using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    [Index(nameof(Plate), IsUnique = true)]
    public class Ships
    {
        [Key]
        public string? Plate { get; set; }
        public string? Model { get; set; }
        public int? Capacity { get; set; }
        public int? Crew { get; set; }
        public string? State { get; set; }
        public int ClientID { get; set; }
        [ForeignKey(nameof(ClientID))]
        public virtual Client Clients { get; set; }
        public virtual ICollection<RepairShip> Reports { get; set; }

    }

}
