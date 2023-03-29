using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Application
{
    public class ApplicationPostDTO
    {
        public int Id { get; set; }
        public string Motivation { get; set; } = null!;
        public bool ApprovedStatus { get; set; }

        //// Relationships
        public int UserId { get; set; }
        //public User User { get; set; } = null!;
        public int ProjectId { get; set; }
        //public Project Project { get; set; } = null!;
    }
}
