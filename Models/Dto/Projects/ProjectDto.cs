namespace Lagalt_Backend.Models.Dto.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Field { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public string Progress { get; set; } = null!;
        public string Tags { get; set; } = null!;
        public string Owner { get; set; }
        public List<int> Contributors { get; set; } = new List<int>();
        public List<int> Images { get; set; } = new List<int>();
    }
}
