using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIRCOM.Models
{
    public class Ships
    {
        [Key]
        public string? Plate { get; set; }
        public string? Model { get; set; }
        public int Capacity { get; set; }
        public int Crew { get; set; }
        public int ClientID { get; set; }
        [ForeignKey(nameof(ClientID))]
        public virtual Client? Clients { get; set; }

    }

}
