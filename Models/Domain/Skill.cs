using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Skill")]
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Relationships
        public ICollection<User> Users { get; set; }
    }
}