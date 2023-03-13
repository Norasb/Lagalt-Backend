using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
