using Microsoft.EntityFrameworkCore;

namespace ConstructionReact.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Construction> Constructions { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
