﻿using Microsoft.AspNetCore.Mvc;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.PortfolioServices;
using System.Net;
using AutoMapper;
using Lagalt_Backend.Models.Dto.Portfolio;
using Microsoft.AspNetCore.Authorization;

namespace Lagalt_Backend.Controllers
{
    [Route("api/portfolios")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;

        public PortfolioController(IPortfolioService portfolioService, IMapper mapper)
        {
            _portfolioService = portfolioService;
            _mapper = mapper;
        }


        
        // GET: api/Portfolios
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PortfolioDTO>>> GetPortfolios()
        {
            return Ok(_mapper.Map<List<PortfolioDTO>>(await _portfolioService.GetAllAsync()));
        }

        [HttpPost]
        public async Task<ActionResult<Portfolio>> PostPortfolio(PortfolioPostDTO portfolioDto)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(portfolioDto);
            await _portfolioService.AddAsync(portfolio);
            return CreatedAtAction("GetPortfolio", new { id = portfolio.Id }, portfolio);
        }
        
        // GET: api/Portfolios/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PortfolioDTO>> GetPortfolio(int id)
        {
            try
            {
                return Ok(_mapper.Map<PortfolioDTO>(await _portfolioService.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
            }
        }
        
        // PUT: api/Portfolios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> PutPortfolio(string userId, PortfolioPutDTO portfolioDto)
        {
            if (id != portfolioDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _portfolioService.UpdateAsync(_mapper.Map<Portfolio>(portfolioDto));
                return NoContent();

            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = ((int)HttpStatusCode.NotFound)
                });
            };
        }
        
        // POST: api/Portfolios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        // DELETE: api/Portfolios/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            try
            {
                await _portfolioService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    });
            }
        }
    }
}
        