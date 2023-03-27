using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Link;
using Lagalt_Backend.Services.Links;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;

        public LinkController(ILinkService linkService, IMapper mapper)
        {
            _linkService = linkService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LinkDto>>> GetAllLinks()
        {
            return Ok(
                _mapper.Map<List<LinkDto>>(
                await _linkService.GetAllAsync()));
        }

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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddLink(LinkPostDto linkDto)
        {
            Link link = _mapper.Map<Link>(linkDto);
            await _linkService.AddAsync(link);
            return CreatedAtAction("GetSkillById", new { id = link.Id }, link);
        }

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
