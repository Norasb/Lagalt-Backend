using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Application
{
    public class ApplicationPostDTO
    {
        public string Motivation { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int ProjectId { get; set; }
    }
}
