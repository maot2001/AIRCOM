using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public virtual DbSet<Worker> Workers { get; set; } = null!;
        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Ships> Ships { get; set; } = null!;
        public virtual DbSet<Repair> Repairs { get; set; } = null!;
        public virtual DbSet<RepairInstallation> RepairInstallations { get; set; } = null!;
        public virtual DbSet<RepairShip> RepairShips { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Installation>()
           .HasKey(i => new { i.InstallationID, i.AirportID });

            modelBuilder.Entity<ServicesInstallation>()
           .HasKey(si => new { si.InstallationID, si.AirportID, si.Code });

            modelBuilder.Entity<on_site>()
           .HasKey(os => new { os.InstallationID, os.AirportID, os.Code, os.ClientID, os.Fecha });

            modelBuilder.Entity<RepairInstallation>()
            .HasKey(ri => new { ri.InstallationID, ri.AirportID, ri.RepairID });

            modelBuilder.Entity<RepairShip>()
           .HasKey(rs => new { rs.InstallationID, rs.AirportID, rs.RepairID, rs.Plate, rs.Init });

            modelBuilder.Entity<History>()
           .HasKey(h => new { h.AirportID, h.Plate, h.Date });

            modelBuilder.Entity<Worker>().HasData(
            new Worker
            {
                WorkerID = 1,
                Name = "Marco",
                Email = "marco@gmail.com",
                Pwd = "1234",
                Type = "Mechanic"
            },
            new Worker
            {
                WorkerID = 2,
                Name = "Javier",
                Email = "javier@gmail.com",
                Pwd = "1234",
                Type = "Security"
            },
            new Worker
            {
                WorkerID = 3,
                Name = "Carla",
                Email = "carla@gmail.com",
                Pwd = "1234",
                Type = "Direction"
            });

            modelBuilder.Entity<Client>().HasData(
            new Client
            {
                ClientID = 1,
                Name = "Alejandro",
                Type = "Presidente",
                Nationality = "Cuba",
                Email = "alejo@gmail.com",
                Pwd = "1234"
            });
        }
    }
}
