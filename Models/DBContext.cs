using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Airoport> Airoports { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Ships> Ships { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Installation>()
           .HasKey(i => new { i.InstallationID, i.AiroportID });

            modelBuilder.Entity<ServicesInstallation>()
           .HasKey(si => new { si.InstallationID, si.AiroportID, si.Code });

            modelBuilder.Entity<on_site>()
           .HasKey(os => new { os.InstallationID, os.AiroportID, os.Code, os.ClientID, os.Fecha });
        }
    }
}
