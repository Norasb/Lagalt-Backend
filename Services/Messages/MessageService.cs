using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Services.Messages
{
    public class MessageService : IMessageService
    {

        private readonly LagAltDbContext _context;

        public MessageService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Message obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                throw new Exception("Message not found");
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Message>> GetAllAsync()
        {
            return await _context.Messages
                .ToListAsync();
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages
                .Where(x => x.Id == id)
                .FirstAsync();
        }

        public async Task UpdateAsync(Message obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
