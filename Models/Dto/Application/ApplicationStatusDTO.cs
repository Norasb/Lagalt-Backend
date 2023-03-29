namespace Lagalt_Backend.Models.Dto.Application
{
    public class ApplicationStatusDTO
    {
        public string Motivation { get; set; } = null!;
        public bool ApprovalStatus { get; set; }

        public string ProjectTitle { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
