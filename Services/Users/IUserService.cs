using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.UserServices
{
    public interface IUserService : ICrudService<User, string>
    {
        public Task<bool> UserExists(string id);
        Task<ICollection<Application>> GetApplicationsInUser(string userId);

        Task<ICollection<Project>> GetProjectsInUser(string userId);
        Task<Portfolio> GetPortfolioInUser(string userId);
        Task<ICollection<Project>> GetOnlyOwnedProjectsInUser(string userId);

    }
}
