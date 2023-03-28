using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Portfolio
{
    public class PortfolioPutDTO
    {
        public string? Description { get; set; } = null!;
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
