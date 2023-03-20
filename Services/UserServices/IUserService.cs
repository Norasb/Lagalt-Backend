using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.UserServices
{
    public interface IUserService : ICrudService<User, int>
    {
        public Task<bool> UserExists(int id);
    }
}
