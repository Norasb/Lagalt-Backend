using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lagalt_Backend.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly LagAltDbContext _context;

        public UserService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Skills)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            if (!await UserExists(id))
            {
                throw new Exception("User not found");
            }

            return await _context.Users.FindAsync(id);
        }

        public async Task<ICollection<Application>> GetApplicationsInUser(string userId)
        {
            if (!await UserExists(userId))
            {
                throw new Exception("User does not exist");
            }

            return await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Applications)
                .Select(u => u.Applications)
                .FirstAsync();

        }

        public async Task AddAsync(User obj)
        {
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User obj)
        {
            if (!await UserExists(obj.Id))
            {
                throw new Exception("User not found");

            }
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
