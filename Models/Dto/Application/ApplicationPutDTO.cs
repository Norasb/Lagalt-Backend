using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Application
{
    public class ApplicationPutDTO
    {
        public int Id { get; set; }
        public string Motivation { get; set; } = null!;

        // Relationships
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
