using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    }
}
