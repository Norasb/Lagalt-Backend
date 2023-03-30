using Microsoft.AspNetCore.Mvc;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.PortfolioServices;
using System.Net;
using AutoMapper;
using Lagalt_Backend.Models.Dto.Portfolio;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/portfolios")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// <summary>
        /// Get all portfolios from the database.
        /// </summary>
        /// <returns>List<Portfolio></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PortfolioDTO>>> GetPortfolios()
        {
            return Ok(_mapper.Map<List<PortfolioDTO>>(await _portfolioService.GetAllAsync()));
        }

        /// <summary>
        /// Add a portfolio to the database.
        /// </summary>
        /// <param name="portfolioDto">PortfolioPostDTO</param>
        /// <returns>PortfolioDTO</returns>
        [HttpPost]
        public async Task<ActionResult<Portfolio>> PostPortfolio(PortfolioPostDTO portfolioDto)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(portfolioDto);
            await _portfolioService.AddAsync(portfolio);
            return CreatedAtAction("GetPortfolio", new { id = portfolio.Id }, portfolio);
        }
        
        // GET: api/Portfolios/5
        /// <summary>
        /// Get a specific portfolio by ID.
        /// </summary>
        /// <param name="id">Portfolio ID</param>
        /// <returns>PortfolioDTO</returns>
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
        /// <summary>
        /// Update a portfolio in the database by ID.
        /// </summary>
        /// <param name="id">Portfolio ID</param>
        /// <param name="portfolioDto">PortfolioPutDTO</param>
        /// <returns>BadRequest if the ID in the DTO does not match the ID from the URL.
        /// NoContent if the portfolio is updated successfully.
        /// NotFound if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPortfolio(int id, PortfolioPutDTO portfolioDto)
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

        // DELETE: api/Portfolios/5
        /// <summary>
        /// Delete a portfolio from the database by ID.
        /// </summary>
        /// <param name="id">Portfolio ID</param>
        /// <returns>NoContent if the request is successful.
        /// NotFound if the request fails.</returns>
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
        