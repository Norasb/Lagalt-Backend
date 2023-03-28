using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.ApplicationServices
{
    public interface IApplicationService : ICrudService<Application, int>
    {
        public Task<bool> ApplicationExists(int id);
        public Task<ICollection<Application>> GetNotApprovedApplications(int projectId);
    }
}
