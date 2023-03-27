using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Models
{
    public class LagAltDbContext : DbContext
    {
        public LagAltDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Portfolio> Portfolios { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Application> Applications { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Link> Links { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
