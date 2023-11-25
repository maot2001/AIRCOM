using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    [Index(nameof(ID), IsUnique = true)]
    public class on_site
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Fecha { get; set; }
        public float Price { get; set; }
        public int? Stars { get; set; } = 0;
        public string? Comment { get; set; }
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Installation Installation { get; set; }
        /*public int? AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }*/
        public int Code { get; set; }
        [ForeignKey(nameof(Code))]
        public virtual CustomerService CustomerService { get; set; }
        public int ClientID { get; set; }
        [ForeignKey(nameof(ClientID))]
        public virtual Client Client { get; set; }
    }
}
