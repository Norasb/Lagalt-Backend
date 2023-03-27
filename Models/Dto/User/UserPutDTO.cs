namespace Lagalt_Backend.Models.Dto.User
{
    public class UserPutDTO
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
