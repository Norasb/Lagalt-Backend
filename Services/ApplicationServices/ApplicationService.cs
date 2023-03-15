using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.ApplicationServices
{
    public class ApplicationService : IApplicationService
    {
        public Task AddAsync(Application obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApplicationExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Application>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Application> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Application obj)
        {
            throw new NotImplementedException();
        }
    }
}
