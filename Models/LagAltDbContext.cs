using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Models
{
    public class LagAltDbContext : DbContext
    {
        public LagAltDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
