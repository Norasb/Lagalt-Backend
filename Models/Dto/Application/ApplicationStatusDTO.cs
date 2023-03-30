namespace Lagalt_Backend.Models.Dto.Application
{
    public class ApplicationStatusDTO
    {
        public int Id { get; set; }
        public bool ApprovalStatus { get; set; }
        public string UserName { get; set; } = null!;
    }
}
