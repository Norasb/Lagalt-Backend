using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.PortfolioServices;

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpPost]
        public async Task<ActionResult<Portfolio>> PostPortfolio(Portfolio portfolio)
        {
            await _portfolioService.AddAsync(portfolio);
            return CreatedAtAction("GetPortfolioByID", new { id = portfolio.Id }, portfolio);
        }
        /*
        // GET: api/Portfolios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolios()
        {
          if (_portfolioServices.Portfolios == null)
          {
              return NotFound();
          }
            return await _portfolioServices.Portfolios.ToListAsync();
        }

        // GET: api/Portfolios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id)
        {
          if (_portfolioServices.Portfolios == null)
          {
              return NotFound();
          }
            var portfolio = await _portfolioServices.Portfolios.FindAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }

        // PUT: api/Portfolios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolio(int id, Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest();
            }

            _portfolioServices.Entry(portfolio).State = EntityState.Modified;

            try
            {
                await _portfolioServices.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Portfolios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        // DELETE: api/Portfolios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            if (_portfolioServices.Portfolios == null)
            {
                return NotFound();
            }
            var portfolio = await _portfolioServices.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }

            _portfolioServices.Portfolios.Remove(portfolio);
            await _portfolioServices.SaveChangesAsync();

            return NoContent();
        }

        private bool PortfolioExists(int id)
        {
            return (_portfolioServices.Portfolios?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
        