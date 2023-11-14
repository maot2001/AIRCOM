using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    [Index(nameof(Code),IsUnique =true)]
    [Index(nameof(Fecha),IsUnique =true)]
    public class on_site
    {
        [Key]
        public DateTime Fecha { get; set; }
        public string? Assessment { get; set; }
        public int Total_price { get; set; }
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Installation? Installation { get; set; }
        public int AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport? Airport { get; set; }
        public int Code { get; set; }
        [ForeignKey(nameof(Code))]
        public virtual CustomerService? CustomerService { get; set; }
        public int ClientID { get; set; }
        [ForeignKey(nameof(ClientID))]
        public virtual Client? Client { get; set; }
    }
}
