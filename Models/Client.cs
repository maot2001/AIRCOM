﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AIRCOM.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    [Index(nameof(ClientID),IsUnique =true)]
    public class Client : IUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Nationality { get; set; }
        public string? Email { get; set; }
        public string? Pwd { get; set; }
        public virtual ICollection<on_site>? On_Sites { get; set; }
        public virtual ICollection<Ships>? Shipss { get; set; }
    }
}
