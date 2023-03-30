using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Services.Projects
{
    public interface IProjectService : ICrudService<Project, int>
    {
        /// <summary>
        /// Get all projects from the database and sort them based on which matches the user's skills the most.
        /// </summary>
        /// <param name="skill">Skill name</param>
        /// <returns>A collection of projects</returns>
        public Task<ICollection<Project>> GetProjectsBySkill(string skill);
        /// <summary>
        /// Get all application in a specific project that has not been approved by Project ID.
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <returns>Collection of applications</returns>
        public Task<ICollection<Application>> GetNotApprovedApplications(int projectId);
        /// <summary>
        /// Update contributors in a project by project ID.
        /// </summary>
        /// <param name="obj">Project ID</param>
        /// <returns>Collection of Application</returns>
        public Task UpdateContributorsAsync(Project obj);
    }
}
