using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Message")]
    public class Message
    {
        public int Id { get; set; }
        public DateTime DOC { get; set; }
        public string Text { get; set; } = null!;

        // Relationships
        public int UserId { get; set; }
        [InverseProperty("Messages")]
        public User User { get; set; } = null!;

        public int ProjectId { get; set; }
        [InverseProperty("Messages")]
        public Project Project { get; set; } = null!;
    }
}
