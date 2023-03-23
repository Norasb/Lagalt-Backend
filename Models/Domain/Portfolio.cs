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
        [InverseProperty("Portfolio")]
        public User User { get; set; } = null!;

        //Relationships
        [InverseProperty("Portfolio")]
        public ICollection<Project>? Projects { get; set; } = new List<Project>();
    }
}
