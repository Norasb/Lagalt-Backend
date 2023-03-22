namespace Lagalt_Backend.Models.Domain
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Relationships
        public ICollection<Project>? Projects { get; set; }
    }
}
