﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AIRCOM.Models
{
    [Index(nameof(AirportID), IsUnique = true)]
    public class Airport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AirportID { get; set; }
        public string? Name { get; set; }
        public string? Coordinates { get; set; }
        public string? Direction { get; set; }
        public virtual ICollection<Installation>? Installations { get; set; }
        public virtual ICollection<ServicesInstallation>? ServicesInstallations { get; set; }
        public virtual ICollection<on_site>? On_Sites { get; set; }
        public virtual ICollection<RepairShip> RepairShip { get; set; }
        public virtual ICollection<RepairInstallation> RepairInstallation { get; set; }
        public virtual ICollection<History> History { get; set; }
        
    }
}
