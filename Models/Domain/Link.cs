using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    public class Link
    {
        public int Id { get; set; }
        public string URL { get; set; } = null!;

        //Relationships
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [InverseProperty("Links")]
        public Project Project { get; set; } = null!;
    }
}
