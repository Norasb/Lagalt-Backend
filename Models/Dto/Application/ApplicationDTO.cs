using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Application
{
    public class ApplicationDTO
    {
        public string Motivation { get; set; } = null!;
        public bool ApprovalStatus { get; set; }
        public string UserName { get; set; } = null!;
        public string ProjectTitle { get; set; } = null!;
    }
}
