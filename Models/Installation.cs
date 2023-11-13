using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models
{
    public class Installation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstallationID { get; set; }
        public string? Name { get; set; }
        public string? Direction { get; set; }
        public string? Ubication { get; set; }
        public int AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }
        public virtual ICollection<ServicesInstallation>? ServicesInstallations { get; set; }
        public virtual ICollection<on_site>? on_sites { get; set; }
    }
}
