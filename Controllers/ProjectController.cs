using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    [Route("projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            return Ok(
                await _projectService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            try
            {
                return Ok(await _projectService.GetByIdAsync(id));

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
        public async Task<ActionResult> AddProject(Project project)
        {
           await _projectService.AddAsync(project);
            return CreatedAtAction("GetProjectById", new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Project project)
        {
            try
            {
                await _projectService.UpdateAsync(project);
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
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
                return NoContent();
            } 
            catch (Exception ex)
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
