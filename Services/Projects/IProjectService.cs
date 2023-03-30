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
        public Task<ICollection<Application>> GetNotApprovedApplications(int projectId);
        public Task UpdateContributorsAsync(Project obj);
    }
}
