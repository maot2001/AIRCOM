using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models
{
    public class Installation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstallationID { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public string Ubication { get; set; }
        public int AiroportID { get; set; }
        [ForeignKey(nameof(AiroportID))]

        public virtual Airoport Airoport { get; set; }
        public virtual ICollection<ServicesInstallation> ServicesInstallations { get; set; }
        public virtual ICollection<on_site> on_sites { get; set; }
    }
}
