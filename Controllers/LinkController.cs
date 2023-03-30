using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Link;
using Lagalt_Backend.Services.Links;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/links")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;

        public LinkController(ILinkService linkService, IMapper mapper)
        {
            _linkService = linkService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all links in the database.
        /// </summary>
        /// <returns>List of links</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LinkDto>>> GetAllLinks()
        {
            return Ok(
                _mapper.Map<List<LinkDto>>(
                await _linkService.GetAllAsync()));
        }

        /// <summary>
        /// Get a specific link from the database by ID.
        /// </summary>
        /// <param name="id">Link ID</param>
        /// <returns>LinkDTO if the request is successfull.
        /// NotFound if the request fails.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<LinkDto>> GetLinkById(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<LinkDto>(
                    await _linkService.GetByIdAsync(id)));

            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    });
            }
        }

        /// <summary>
        /// Add a link to the database.
        /// </summary>
        /// <param name="linkDto">LinkDTO</param>
        /// <returns>LinkDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddLink(LinkPostDto linkDto)
        {
            Link link = _mapper.Map<Link>(linkDto);
            await _linkService.AddAsync(link);
            return CreatedAtAction("GetSkillById", new { id = link.Id }, link);
        }

        /// <summary>
        /// Update a link in the database.
        /// </summary>
        /// <param name="id">Link ID</param>
        /// <param name="linkDto">LinkDTO</param>
        /// <returns>NoContent if the update is successfull.
        /// NotFound if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateLink(int id, LinkPutDto linkDto)
        {

            Link existingLink = _linkService.GetByIdAsync(id).Result;
            existingLink.URL = linkDto.URL;

            try
            {
                await _linkService.UpdateAsync(_mapper.Map<Link>(existingLink));
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(
                     new ProblemDetails()
                     {
                         Detail = ex.Message,
                         Status = ((int)HttpStatusCode.NoContent)
                     });
            }
        }

        /// <summary>
        /// Delete a link in the database.
        /// </summary>
        /// <param name="id">Link ID</param>
        /// <returns>NoContent if the deletion is successfull.
        /// NotFound if the deletion fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteLink(int id)
        {
            try
            {
                await _linkService.DeleteByIdAsync(id);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NoContent)
                    });
            }
        }
    }
}
