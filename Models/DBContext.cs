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
    }
}
