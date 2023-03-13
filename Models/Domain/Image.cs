using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Image")]
    public class Image
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
