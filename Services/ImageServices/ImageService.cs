using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly LagAltDbContext _context;

        public ImageService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Image obj)
        {
            await _context.Images.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if(image == null)
            {
                throw new Exception("Image not found");
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Image>> GetAllAsync()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            if (!await ImageExists(id))
            {
                throw new Exception("Image not found");
            }

            return await _context.Images.FindAsync(id);
        }

        public async Task UpdateAsync(Image obj)
        {
            if (!await ImageExists(obj.Id))
            {
                throw new Exception("Image not found");
            }

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ImageExists(int id)
        {
            return await _context.Images.AnyAsync(i => i.Id == id);
        }
    }
}
