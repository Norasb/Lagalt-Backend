using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Lagalt_Backend.Services.PortfolioServices
{
    public class PortfolioService: IPortfolioService
    {
        private readonly LagAltDbContext _context;

        public PortfolioService(LagAltDbContext context)
        {
            _context = context;
        }
        
        public async Task<ICollection<Portfolio>> GetAllAsync()
        {
            return (ICollection<Portfolio>)await _context.Portfolios.ToListAsync();
        }
       

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            if (!await PortfolioExists(id))
            {
                throw new Exception("Portfolio not found");
            }

            return await _context.Portfolios.FindAsync(id);
        }

        public async Task AddAsync(Portfolio obj)
        {
            await _context.Portfolios.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio obj)
        {
            if (!await PortfolioExists(obj.Id))
            {
                throw new Exception("Portfolio not found");

            }
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);

            if (portfolio == null)
            {
                throw new Exception("Portfolio not found");
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> PortfolioExists(int id)
        {
            return await _context.Portfolios.AnyAsync(u => u.Id == id);
        }
    }

}
