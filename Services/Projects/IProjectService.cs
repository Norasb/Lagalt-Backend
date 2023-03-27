using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Services.Projects
{
    public interface IProjectService : ICrudService<Project, int>
    {
        public Task<ICollection<Project>> GetProjectsBySkill(string skill);
    }
}
