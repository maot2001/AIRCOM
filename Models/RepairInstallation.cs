using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    public class RepairInstallation
    {
        public int Price { get; set; }
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Airoport? Airoport { get; set; }
        public int AiroportID { get; set; }
        [ForeignKey(nameof(AiroportID))]
        public virtual Installation? Installation { get; set; }
        public int RepairID { get; set; }
        [ForeignKey(nameof(RepairID))]
        public virtual Repair? Repair { get; set; }
    }
}
