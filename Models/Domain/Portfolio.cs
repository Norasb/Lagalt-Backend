using Microsoft.Build.Construction;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        // Relationships
        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public ICollection<Project> Projects { get; set; }


    }
}
