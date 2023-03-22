using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        // Relationships
        [InverseProperty("User")]
        public Portfolio? Portfolio { get; set; }

        [InverseProperty("Owner")]
        public ICollection<Project>? OwnedProjects { get; set; } = new List<Project>();
        
        [InverseProperty("Contributors")]
        public ICollection<Project>? ContributedProjects { get; set; } = new List<Project>();

        [InverseProperty("User")]
        public ICollection<Message>? Messages { get; set; } = new List<Message>();

        [InverseProperty("User")]
        public ICollection<Application>? Applications { get; set; } = new List<Application>();

        [InverseProperty("Users")]
        public ICollection<Skill>? Skills { get; set; } = new List<Skill>();
    }
}
