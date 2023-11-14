using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(Init),IsUnique = true)]
    

    public class RepairShip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Init { get; set; }
        public DateTime Finish { get; set; }
        public string? State { get; set; }
        public int Time { get; set; }
        public int Valoration { get; set; }
        public int Price { get; set; }
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Airport? Airoport { get; set; }
        public int AiroportID { get; set; }
        [ForeignKey(nameof(AiroportID))]
        public virtual Installation? Installation { get; set; }
        public int RepairID { get; set; }
        [ForeignKey(nameof(RepairID))]
        public virtual Repair? Repair { get; set; }
        public int Plate { get; set; }
        [ForeignKey(nameof(Plate))]
        public virtual Ships Ships { get; set; }
    }
}
