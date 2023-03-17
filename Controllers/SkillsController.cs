using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.Skills;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    [Route("skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAllSkills()
        {
            return Ok(
                await _skillService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkillById(int id)
        {
            try
            {
                return Ok(await _skillService.GetByIdAsync(id));

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
        public async Task<ActionResult> AddSkill(Skill skill)
        {
            await _skillService.AddAsync(skill);
            return CreatedAtAction("GetSkillById", new { id = skill.Id }, skill);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSkill(int id, Skill skill)
        {
            try
            {
                await _skillService.UpdateAsync(skill);
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
