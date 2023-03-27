﻿using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;
using Lagalt_Backend.Models.Dto.User;
using Lagalt_Backend.Services.Projects;
using Lagalt_Backend.Services.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly ISkillService _skillService;

        public ProjectController(IProjectService projectService, IMapper mapper, ISkillService skillService)
        {
            _projectService = projectService;
            _mapper = mapper;
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            return Ok(
                _mapper.Map<List<ProjectDto>>(
                await _projectService.GetAllAsync()));
        }

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

  


        [HttpPost]
        public async Task<ActionResult> AddProject(ProjectPostDto projectDto)
        {
            var skills = await _skillService.GetSkillsByIdAsync(projectDto.Skills);
            var newProject = new Project
            {
                Field = projectDto.Field,
                Title = projectDto.Title,
                Description = projectDto.Description,
                Caption = projectDto.Caption,
                DOC = projectDto.DOC,
                Progress = projectDto.Progress,
                UserId = projectDto.UserId,
                Skills = skills
            };

            Project project = _mapper.Map<Project>(newProject);
            await _projectService.AddAsync(project);

            return CreatedAtAction("GetProjectById", new { id = project.Id }, projectDto);
        }

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

        [HttpGet("skill")]
        public async Task<IActionResult> GetProjectsBySkill(string id)
        {
            return Ok(
                _mapper.Map<List<ProjectDto>>(
                await _projectService.GetProjectsBySkill(id)));
        }

    }
}
