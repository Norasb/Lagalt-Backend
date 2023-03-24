using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Application
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string Motivation { get; set; } = null!;
        public bool ApprovalStatus { get; set; }

        // Relationships
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
