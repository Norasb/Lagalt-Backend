using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;

namespace Lagalt_Backend.Services.Links
{
    public interface ILinkService : ICrudService<Link, int>
    {
        public Task<ICollection<Link>> GetLinksByIdAsync(List<int> linkIds);
        
    }
}
