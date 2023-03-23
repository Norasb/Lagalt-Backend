using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services.Links
{
    public class LinkService : ILinkService
    {
        private readonly LagAltDbContext _context;

        public LinkService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Link obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var link = await _context.Links.FindAsync(id);

            if (link == null)
            {
                throw new Exception("Link not found");
            }

            _context.Links.Remove(link);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Link>> GetAllAsync()
        {
            return await _context.Links
                .ToListAsync();
        }

        public async Task<Link> GetByIdAsync(int id)
        {
            return await _context.Links
                .Where(l => l.Id == id)
                .FirstAsync();
        }

        public async Task UpdateAsync(Link obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
