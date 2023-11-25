﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    [Index(nameof(WorkerID),IsUnique =true)]
    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerID { get; set; }
        public string CI { get; set; }
        public string Name { get; set; }
        public int AirportID { get; set; }
        [ForeignKey(nameof(AirportID))]
        public virtual Airport Airport { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Nationality { get; set; }
    }
}