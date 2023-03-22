using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.ApplicationServices;
using System.Net;
using AutoMapper;
using Lagalt_Backend.Models.Dto.Application;

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public ApplicationController(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService;
            _mapper = mapper;
        }

        // GET: api/Application
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetApplications()
        {
            return Ok(
                _mapper.Map<List<ApplicationDTO>>(
                await _applicationService.GetAllAsync()));
        }

        // GET: api/Application/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetApplication(int id)
        {
            try
            {
                return Ok(_mapper.Map<ApplicationDTO>(await _applicationService.GetByIdAsync(id)));
            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
            }
        }

        // PUT: api/Application/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, ApplicationPutDTO applicationDto)
        {
            if (id != applicationDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _applicationService.UpdateAsync(_mapper.Map<Application>(applicationDto));
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
            }
        }

        // POST: api/Application
        [HttpPost]
        public async Task<ActionResult<ApplicationPostDTO>> PostApplication(ApplicationPostDTO applicationDto)
        {
            Application application = _mapper.Map<Application>(applicationDto);
            await _applicationService.AddAsync(application);
            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        // DELETE: api/Application/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                await _applicationService.DeleteByIdAsync(id);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
            }

            
        }
    }
}
