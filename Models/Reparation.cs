using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    public class Reparation
    {
        [Key]
        public int Code { get; set; }
        public string Descriptipon { get; set; }
        public int Price { get; set; }

        public virtual ICollection<InstReparation> InstReparation { get; set; } 


    }

}
