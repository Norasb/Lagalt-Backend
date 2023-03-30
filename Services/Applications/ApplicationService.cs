using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services.ApplicationServices
{
    public class ApplicationService : IApplicationService
    {
        private readonly LagAltDbContext _context;

        public ApplicationService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Application obj)
        {
            var application = new Application
            {
                Motivation = obj.Motivation,
                ApprovalStatus = false
            };

            var user = _context.Users.SingleOrDefault(u => u.Id == obj.UserId);
            application.User = user;

            var project = _context.Projects.SingleOrDefault(p => p.Id == obj.ProjectId);
            application.Project = project;


            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                throw new Exception("User not found");
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Application>> GetAllAsync()
        {
            return await _context.Applications
                .Include(a => a.User)
                .Include(a => a.Project)
                .ToListAsync();
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            if (!await ApplicationExists(id))
            {
                throw new Exception("Application not found");
            }

            return await _context.Applications
                .Where(a => a.Id == id)
                .Include(a => a.User)
                .Include(a => a.Project)
                .FirstAsync();
        }

        public async Task UpdateAsync(Application obj)
        {
            if (!await ApplicationExists(obj.Id))
            {
                throw new Exception("Application not found");
            }

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ApplicationExists(int id)
        {
            return await _context.Applications.AnyAsync(a => a.Id == id);
        }
    }
}
