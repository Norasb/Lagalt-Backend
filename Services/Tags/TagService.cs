using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services.Tags
{
    public class TagService : ITagService
    {

        private readonly LagAltDbContext _context;

        public TagService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tag obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var tags = await _context.Tags.FindAsync(id);

            if (tags == null)
            {
                throw new Exception("Tag not found");
            }
            _context.Tags.Remove(tags);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Tag>> GetAllAsync()
        {
            return await _context.Tags
                .ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Tags
                .Where(t => t.Id == id)
                .FirstAsync();
        }

        public async Task UpdateAsync(Tag obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
