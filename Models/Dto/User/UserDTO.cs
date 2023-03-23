

using Lagalt_Backend.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Dto.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        // Relationships
        public int Portfolio { get; set; }
        public List<int> OwnedProjects { get; set; }

        public List<int> ContributedProjects { get; set; }

        public List<int> Messages { get; set; }

        public List<int> Applications { get; set; }

        public List<int> Skills { get; set; }
    }
}
