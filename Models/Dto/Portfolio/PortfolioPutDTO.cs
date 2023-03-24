using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Portfolio
{
    public class PortfolioPutDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        // Relationships
        public int UserId { get; set; }
        //public User User { get; set; }

        public int? ProjectId { get; set; }
        //public ICollection<Project>? Projects { get; set; }
    }
}
