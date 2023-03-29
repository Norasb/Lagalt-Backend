using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Skill;

namespace Lagalt_Backend.Models.Dto.User
{
    public class UserPutDTO
    {
        public string Id { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string> Skills { get; set; } = new List<string>();
    }
}
