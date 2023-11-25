using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    [Index(nameof(Code), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class CustomerService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ServicesInstallation>? ServicesInstallations { get; set; }
        public virtual ICollection<on_site>? On_Sites { get; set; }
    } 
}
