using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;

namespace Lagalt_Backend.Models.Dto.Portfolio
{
    public class PortfolioDTO
    {
        public string Description { get; set; } = null!;
        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    }
}
