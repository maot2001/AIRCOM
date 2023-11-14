using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models
{
    public class CustomerService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ServicesInstallation>? ServicesInstallations { get; set; }
        public virtual ICollection<on_site>? On_Sites { get; set; }
    } 
}
