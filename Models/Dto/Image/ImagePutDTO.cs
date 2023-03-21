using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Image
{
    public class ImagePutDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;

        public int ProjectId { get; set; }
    }
}
