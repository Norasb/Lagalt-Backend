using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;

namespace Lagalt_Backend.Services.Projects
{
    public class ProjectService : IProjectService
    {

        private readonly LagAltDbContext _context;

        public ProjectService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddProjectWithSkills(Project obj)
        {
            List<Skill> skills = obj.Skills.ToList()
                .Select(skill => _context.Skills
                .Where(m => m.Id == skill.Id).First())
                .ToList();
            obj.Skills = skills;
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(Project obj)
        {
            List<Skill> skills = obj.Skills.ToList()
                .Select(skill => _context.Skills
                .Where(s => s.Id == skill.Id).First())
                .ToList();

            obj.Skills = skills;

            //List<Tag> tags = obj.Tags.ToList()
            //    .Select(tag => _context.Tags
            //    .Where(t => t.Id == tag.Id).First())
            //    .ToList();

            //obj.Tags = tags;

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
                .Include(p => p.Skills)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Where(p => p.Id == id)
                .FirstAsync();
        }

        public async Task UpdateAsync(Project obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
