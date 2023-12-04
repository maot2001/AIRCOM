using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{

    [Index(nameof(ID),IsUnique =true)]
    public class Depend
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string State { get; set; }
        public int PrimaryID { get; set; }
        public int SecondID { get; set; }
    }
}
