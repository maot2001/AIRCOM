using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AIRCOM.Models
{
    public class Airoport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AirportID { get; set; }
        public string? Name { get; set; }
        public string? Coordinates { get; set; }
        public string? Direction { get; set; }

        public virtual ICollection<Installation>? Installations { get; set; }
        public virtual ICollection<ServicesInstallation>? ServicesInstallations { get; set; }
        public virtual ICollection<on_site>? On_Sites { get; set; }
        
    }
}
