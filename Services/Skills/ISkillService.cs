using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.Skills
{
    public interface ISkillService : ICrudService<Skill, int>
    {
        public Task<ICollection<Skill>> GetSkillsByIdAsync(List<int> skillIds);
    }
}
