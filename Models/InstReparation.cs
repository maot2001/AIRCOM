using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    public class InstReparation
    {
        public int AiroportID { get; set; }
        [ForeignKey(nameof(AiroportID))]
        public virtual Airport Airoports { get; set; }
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Installation Installations { get; set; }
        public int Code { get; set; }
        [ForeignKey(nameof(Code))]
        public virtual Reparation ReparationS { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Price { get; set; }
    }
}
