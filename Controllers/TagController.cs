using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Tag;
using Lagalt_Backend.Services.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/tags")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all tags from the database.
        /// </summary>
        /// <returns>List<TagDTO></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
        {
            return Ok(
                _mapper.Map<List<TagDto>>(
                await _tagService.GetAllAsync()));
        }

        /// <summary>
        /// Get a specific tag from the database by ID.
        /// </summary>
        /// <param name="id">Tag ID</param>
        /// <returns>TagDTO or NotFound if the request fails.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetTagById(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<TagDto>(
                    await _tagService.GetByIdAsync(id)));

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
        /// Add a tag to the database.
        /// </summary>
        /// <param name="tagDto">TagPostDTO</param>
        /// <returns>TagDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddTag(TagPostDto tagDto)
        {
            Tag tag = _mapper.Map<Tag>(tagDto);
            await _tagService.AddAsync(tag);
            return CreatedAtAction("GetSkillById", new { id = tag.Id }, tag);
        }

        /// <summary>
        /// Update a tag in the database by ID.
        /// </summary>
        /// <param name="id">Tag ID</param>
        /// <param name="tagDto">TagPutDTO</param>
        /// <returns>NoContent if the update is successful. 
        /// NotFound if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateTags(int id, TagPutDto tagDto)
        {

            Tag existingTag = _tagService.GetByIdAsync(id).Result;
            existingTag.Name = tagDto.Name;

            try
            {
                await _tagService.UpdateAsync(_mapper.Map<Tag>(existingTag));
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
        /// Delete a tag from the database by ID.
        /// </summary>
        /// <param name="id">Tag ID</param>
        /// <returns>NoContent if the request is successful. 
        /// NotFound if the request fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteTag(int id)
        {
            try
            {
                await _tagService.DeleteByIdAsync(id);
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

