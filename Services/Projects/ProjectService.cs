using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services.Projects
{
    public class ProjectService : IProjectService
    {

        private readonly LagAltDbContext _context;

        public ProjectService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Project obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                throw new EntryPointNotFoundException();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Project>> GetAllAsync()
        {
            return await _context.Projects
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Where(p => p.Id == id)
                .FirstAsync();
        }

        public async Task<Project> GetByUserIdAsync(int userid)
        {
            return await _context.Projects
                .Where(p => p.UserId == userid)
                .FirstAsync();
        }

        public async Task UpdateAsync(Project obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
