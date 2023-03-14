using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Application")]
    public class Application
    {
        public int Id { get; set; }
        public string Motivation { get; set; } = null!;
    }
}
