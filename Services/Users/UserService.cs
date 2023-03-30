using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

            return await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Skills)
                .FirstAsync();
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
        public async Task<ICollection<Project>> GetProjectsInUser(string userId)
        {
            if (!await UserExists(userId))
            {
                throw new Exception("User does not exist");
            }

            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.OwnedProjects)
                    .ThenInclude(p => p.Skills)
                .Include(u => u.OwnedProjects)
                    .ThenInclude(p => p.Tags)
                .Include(u => u.OwnedProjects)
                    .ThenInclude(p => p.Images)
                .Include(u => u.OwnedProjects)
                    .ThenInclude(p => p.Owner)
                .Include(u => u.ContributedProjects)
                    .ThenInclude(p => p.Skills)
                .Include(u => u.ContributedProjects)
                    .ThenInclude(p => p.Tags)
                .Include(u => u.ContributedProjects)
                    .ThenInclude(p => p.Images)
                .Include(u => u.ContributedProjects)
                    .ThenInclude(p => p.Owner)
                .Select(u => new
                {
                    u.OwnedProjects,
                    u.ContributedProjects
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Combine both collections and returns the result.
            var projects = user.OwnedProjects.Concat(user.ContributedProjects).ToList();
            return projects;
        }

        public async Task<Portfolio> GetPortfolioInUser(string userId)
        {
            if (!await UserExists(userId))
            {
                throw new Exception("User does not exist");
            }

            return await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Portfolio)
                    .ThenInclude(p => p.Projects)
                        .ThenInclude(pr => pr.Tags)
                .Include(u => u.Portfolio)
                    .ThenInclude(p => p.Projects)
                        .ThenInclude(pr => pr.Skills)
                .Include(u => u.Portfolio)
                    .ThenInclude(p => p.Projects)
                        .ThenInclude(pr => pr.Owner)
                .Include(u => u.Portfolio)
                    .ThenInclude(p => p.Projects)
                        .ThenInclude(pr => pr.Images)
                .Select(u => u.Portfolio)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Project>> GetOnlyOwnedProjectsInUser(string userId)
        {
            if (!await UserExists(userId))
            {
                throw new Exception("User does not exist");
            }

            return await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.OwnedProjects)
                .Select(u => u.OwnedProjects)
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

            User user = await _context.Users
                .Where(u => u.Id == obj.Id)
                .Include(u => u.Skills)
                .FirstAsync();

            user.Description = obj.Description;

            foreach (var skillName in obj.Skills)
            {
                var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Name == skillName.Name);
                user.Skills.Add(skill);
            }
            
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
