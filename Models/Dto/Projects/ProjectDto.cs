namespace Lagalt_Backend.Models.Dto.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Field { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public string Progress { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public List<string> Tags { get; set; } = new List<string>();
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
