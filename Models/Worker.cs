using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(WorkerId),IsUnique =true)]
    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerId { get; set; }
        public string? Name { get; set; }
        public int? AirportId { get; set; }
        public string? Type { get; set; }
        public string? Email { get; set; }
        public string? Pwd { get; set; }
    }
}
