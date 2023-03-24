using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Project")]
    public class Project
    {
        public int Id { get; set; }
        public string Field { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public DateTime DOC { get; set; }
        public string Progress { get; set; } = null!;
        

        // Relationships
        [InverseProperty("OwnedProjects")]
        public User Owner { get; set; } = null!;

        [ForeignKey("User")]
        public string UserId { get; set; } = null!;

        [InverseProperty("ContributedProjects")]
        public ICollection<User>? Contributors { get; set; } = new List<User>();

        [InverseProperty("Project")]
        public ICollection<Image>? Images { get; set; } = new List<Image>();

        [InverseProperty("Projects")]
        public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
        [InverseProperty("Projects")]
        public ICollection<Skill>? Skills { get; set; } = new List<Skill>();

        [InverseProperty("Projects")]
        public ICollection<Portfolio>? Portfolio { get; set; } = new List<Portfolio>();

        [InverseProperty("Project")]
        public ICollection<Application>? Applications { get; set; } = new List<Application>();

        [InverseProperty("Project")]
        public ICollection<Message>? Messages { get; set; } = new List<Message>();
        [InverseProperty("Project")]
        public ICollection<Link> Links { get; set; } = new List<Link>();
    }
}
