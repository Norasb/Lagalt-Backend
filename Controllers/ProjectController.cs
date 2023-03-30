using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Application;
using Lagalt_Backend.Models.Dto.Projects;
using Lagalt_Backend.Services.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all projects from the database.
        /// </summary>
        /// <returns>List of projects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            return Ok(
                _mapper.Map<List<ProjectDto>>(
                await _projectService.GetAllAsync()));
        }

        /// <summary>
        /// Get a specific project from the database by ID.
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns>ProjectOneDTO</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectOneDto>> GetProjectById(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<ProjectOneDto>(
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

        // GET: api/Application/
        /// <summary>
        /// Get all application in a project that have not been approved yet from the database by project ID. 
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns>List of ApplicationStatusDTO</returns>
        [HttpGet("{id}/notapproved")]
        public async Task<ActionResult<ApplicationDTO>> GetNotApprovedApplicationsInProject(int id)
        {
            return Ok(_mapper.Map<List<ApplicationStatusDTO>>(await _projectService.GetNotApprovedApplications(id)));
        }

        /// <summary>
        /// Add a project to the database.
        /// </summary>
        /// <param name="projectPostDto">ProjectPostDTO</param>
        /// <returns>ProjectDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddProject(ProjectPostDto projectPostDto)
        {
            Project project = _mapper.Map<Project>(projectPostDto);
            await _projectService.AddAsync(project);
            return CreatedAtAction("GetProjectById", new { id = project.Id }, project);
        }

        /// <summary>
        /// Update a project in the database by ID.
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <param name="project">ProjectPostDTO</param>
        /// <returns>NoContent if the update is successful.
        /// NotFound if the request fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
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

        /// <summary>
        /// Delete a project from the database by ID.
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns>NoContent if the request is successful.
        /// NotFound if the request fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
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

        /// <summary>
        /// Gets projects from the database and orders them by which matches the user's skills the most.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>List of projects</returns>
        [HttpGet("skill")]
        public async Task<IActionResult> GetProjectsBySkill(string id)
        {
            return Ok(
                _mapper.Map<List<ProjectDto>>(
                await _projectService.GetProjectsBySkill(id)));
        }

    }
}
