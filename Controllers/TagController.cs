using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Tag;
using Lagalt_Backend.Services.Tags;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
        {
            return Ok(
                _mapper.Map<List<TagDto>>(
                await _tagService.GetAllAsync()));
        }

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

        [HttpPost]
        public async Task<ActionResult> AddTag(TagPostDto tagDto)
        {
            Tag tag = _mapper.Map<Tag>(tagDto);
            await _tagService.AddAsync(tag);
            return CreatedAtAction("GetSkillById", new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

