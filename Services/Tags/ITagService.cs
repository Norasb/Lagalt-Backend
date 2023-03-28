using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;

namespace Lagalt_Backend.Services.Tags
{
    public interface ITagService : ICrudService<Tag, int>
    {
        public Task<ICollection<Tag>> GetTagsByIdAsync(List<int> tagIds);
    }
}
