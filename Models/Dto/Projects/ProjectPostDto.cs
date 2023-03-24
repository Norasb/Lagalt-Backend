using AutoMapper;

namespace Lagalt_Backend.Models.Dto.Projects
{
    public class ProjectPostDto
    {
        public string Field { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public DateTime DOC { get; set; }
        public string Progress { get; set; } = null!;
        public string Tags { get; set; } = null!;
    }
}
