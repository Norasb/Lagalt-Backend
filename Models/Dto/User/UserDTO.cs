

using Lagalt_Backend.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Dto.User
{
    public class UserDTO
    {
        public string UserName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<string> Skills { get; set; } = new List<string>();
    }
}
