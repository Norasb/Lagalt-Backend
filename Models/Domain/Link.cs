namespace Lagalt_Backend.Models.Domain
{
    public class Link
    {
        public int Id { get; set; }
        public string URL { get; set; } = null!;

        //Relationships
        public Project Project { get; set; } = null!;
    }
}
