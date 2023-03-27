using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Skill;
using Lagalt_Backend.Services.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;

        public SkillsController(ISkillService skillService, IMapper mapper)
        {
            _skillService = skillService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetAllSkills()
        {
            return Ok(
                _mapper.Map<List<SkillDto>>(
                await _skillService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDto>> GetSkillById(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<SkillDto>(
                    await _skillService.GetByIdAsync(id)));

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
        public async Task<ActionResult> AddSkill(SkillPostDto skillDto)
        {
            Skill skill = _mapper.Map<Skill>(skillDto);
            await _skillService.AddAsync(skill);
            return CreatedAtAction("GetSkillById", new { id = skill.Id }, skill);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateSkill(int id, SkillPutDto skillDto)
        {

            Skill existingSkill = _skillService.GetByIdAsync(id).Result;
            existingSkill.Name = skillDto.Name;

            try
            {
                await _skillService.UpdateAsync(_mapper.Map<Skill>(existingSkill));
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
        public async Task<ActionResult> DeleteSkill(int id)
        {
            try
            {
                await _skillService.DeleteByIdAsync(id);
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
