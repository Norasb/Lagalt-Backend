using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Application")]
    public class Application
    {
        public int Id { get; set; }
        public string Motivation { get; set; } = null!;

        // Relationships
        public int? UserId { get; set; }
        [InverseProperty("Applications")]
        public User? User { get; set; } = null!;

        public int ProjectId { get; set; }
        [InverseProperty("Applications")]
        public Project Project { get; set; } = null!;
    }
}
