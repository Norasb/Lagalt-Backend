using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.Projects
{
    public interface IProjectService : ICrudService<Project, int>
    {
        public Task<ICollection<Project>> GetProjectsBySkill(string skill);
    }
}
