using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;
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
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            return Ok(
                _mapper.Map<List<ProjectDto>>(
                await _projectService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<ProjectDto>(
                    await _projectService.GetByIdAsync(id)));

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
        public async Task<ActionResult> AddProject(ProjectPostDto projectPostDto)
        {
            Project project = _mapper.Map<Project>(projectPostDto);
            await _projectService.AddAsync(project);
            return CreatedAtAction("GetProjectById", new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(int id, ProjectPutDto project)
        {
            try
            {
                await _projectService.UpdateAsync(_mapper.Map<Project>(project));
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
                await _projectService.DeleteByIdAsync(id);
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
