using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Project")]
    public class Project
    {
        public int Id { get; set; }
        public string Field { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public DateTime DOC { get; set; }
        public string Progress { get; set; }
        public string Tags { get; set; }
    }
}
