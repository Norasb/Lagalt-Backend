using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Image
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;

        // Relationships
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
