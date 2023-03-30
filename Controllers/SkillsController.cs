using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Skill;
using Lagalt_Backend.Services.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/skills")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;

        public SkillsController(ISkillService skillService, IMapper mapper)
        {
            _skillService = skillService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all skills in the database.
        /// </summary>
        /// <returns>List of SkillDTOs</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetAllSkills()
        {
            return Ok(
                _mapper.Map<List<SkillDto>>(
                await _skillService.GetAllAsync()));
        }

        /// <summary>
        /// Get a specific skill from the database by ID.
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <returns>SkillDTO or NotFound if the request fails.</returns>
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

        /// <summary>
        /// Add a skill to the database.
        /// </summary>
        /// <param name="skillDto">SkillPostDTO</param>
        /// <returns>SkillDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddSkill(SkillPostDto skillDto)
        {
            Skill skill = _mapper.Map<Skill>(skillDto);
            await _skillService.AddAsync(skill);
            return CreatedAtAction("GetSkillById", new { id = skill.Id }, skill);
        }

        /// <summary>
        /// Update a skill in the database by ID.
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <param name="skillDto">SkillPutDTO</param>
        /// <returns>NoContent if the update is successful. 
        /// NotFound if the update fails.</returns>
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

        /// <summary>
        /// Delete a skill from the database by ID.
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <returns>NoContent if the request is successful
        /// NotFound if the request fails.</returns>
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
