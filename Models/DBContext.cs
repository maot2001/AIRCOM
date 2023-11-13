using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Airport> Airoports { get; set; }    
        public virtual DbSet<Client> Clients { get; set; }  
        public virtual DbSet<CustomerService> CustomerServices { get; set; }
        public virtual DbSet<Installation> Installments { get; set; }
        public virtual DbSet<on_site> On_Sites { get; set; }
        public virtual DbSet<ServicesInstallation> ServicesInstallations { get; set; }
        public virtual DbSet<Ships> Shipss { get; set; }

        public virtual DbSet<Reparation > Reparation { get; set; }  
        public virtual DbSet<InstReparation> InstReparations { get; set; }

    }
}
