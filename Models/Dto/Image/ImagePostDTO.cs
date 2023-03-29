using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Image
{
    public class ImagePostDTO
    {
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;

        // Relationships
        public int ProjectId { get; set; }
    }
}
