using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.UserServices
{
    public interface IUserService : ICrudService<User, string>
    {
        /// <summary>
        /// Check if the user exists in the database by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>True if the user exists, false if not.</returns>
        public Task<bool> UserExists(string id);
        /// <summary>
        /// Get a user's applications by User ID.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>A collection of applications</returns>
        Task<ICollection<Application>> GetApplicationsInUser(string userId);
        /// <summary>
        /// Get a user's projects by User ID.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>A collection of applications</returns>
        Task<ICollection<Project>> GetProjectsInUser(string userId);
        /// <summary>
        /// Get a user's portfolio by User ID.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>A single portfolio</returns>
        Task<Portfolio> GetPortfolioInUser(string userId);
        /// <summary>
        /// Get project owned by a user by User ID.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Collection of projects</returns>
        Task<ICollection<Project>> GetOnlyOwnedProjectsInUser(string userId);

    }
}
