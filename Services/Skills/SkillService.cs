using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services.Skills
{
    public class SkillService : ISkillService
    {

        private readonly LagAltDbContext _context;

        public SkillService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Skill obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                throw new EntryPointNotFoundException();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Skill>> GetAllAsync()
        {
            return await _context.Skills
                .ToListAsync();
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _context.Skills
                .Where(p => p.Id == id)
                .FirstAsync();
        }

        public async Task UpdateAsync(Skill obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
