using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Tipo { get; set; }
        public string Nationality { get; set; }
        public virtual ICollection<on_site> On_Sites { get; set; }
        public virtual ICollection<Ships> Shipss { get; set; }
    }
}
