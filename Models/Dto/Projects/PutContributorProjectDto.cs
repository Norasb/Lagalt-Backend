namespace Lagalt_Backend.Models.Dto.Projects
{
    public class PutContributorProjectDto
    {
        public int Id { get; set; }
        public List<string> Contributor { get; set; } = new List<string>();
    }
}
