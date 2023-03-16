using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
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
            throw new NotImplementedException();
        }
       

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Portfolio obj)
        {
            await _context.Portfolios.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio obj)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
