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
        public string Tags { get; set; } = null!;
    }
}
