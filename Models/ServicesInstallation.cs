using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(ID), IsUnique = true)]
    public class ServicesInstallation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float? Stars { get; set; }
        public int? Votes { get; set; }
        public bool Active { get; set; } = true;
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Installation Installation { get; set; }
        public int? AirportID { get; set; }
        /*[ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }*/
        public int Code { get; set; }
        [ForeignKey(nameof(Code))]
        public virtual CustomerService CustomerService { get; set; }
        public virtual ICollection<on_site>? On_Sites { get; set; }

    }

}
