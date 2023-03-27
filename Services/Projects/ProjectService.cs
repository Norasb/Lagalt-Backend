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
                .Include(p => p.Owner)
                .Include(p => p.Contributors)
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Include(p => p.Skills)
                .Include(p => p.Links)
                .FirstAsync();
        }

        public async Task UpdateAsync(Project obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Project>> GetProjectsBySkill(string skill)
        {
            return await _context.Projects
                .Where(p => p.Skills.Any(s => s.Name == skill))
                .ToListAsync();
        }
    }
}
