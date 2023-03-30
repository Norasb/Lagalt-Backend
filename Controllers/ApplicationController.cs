using Microsoft.AspNetCore.Mvc;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.ApplicationServices;
using System.Net;
using AutoMapper;
using Lagalt_Backend.Models.Dto.Application;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/applications")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// <summary>
        /// Gets all applications in the database.
        /// </summary>
        /// <returns>List of applications</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetApplications()
        {
            return Ok(
                _mapper.Map<List<ApplicationDTO>>(
                await _applicationService.GetAllAsync()));
        }

        // GET: api/Application/5
        /// <summary>
        /// Gets a specific application from the database by its ID.
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>Application</returns>
        [HttpGet("{id}")]
        [Authorize]
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
        /// <summary>
        /// Updates an application in the database by ID.
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <param name="applicationDto">ApplicationDTO</param>
        /// <returns>
        /// BadRequest if the ID in the DTO does not match the ID from the URL. 
        /// NoContent if the application is successfully updated.
        /// NotFound if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
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
        /// <summary>
        /// Add an application to the database.
        /// </summary>
        /// <param name="applicationDto">ApplicationPostDTO</param>
        /// <returns>ApplicationDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApplicationPostDTO>> PostApplication(ApplicationPostDTO applicationDto)
        {
            Application application = _mapper.Map<Application>(applicationDto);
            await _applicationService.AddAsync(application);
            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        // DELETE: api/Application/5
        /// <summary>
        /// Deletes an application from the database by ID.
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>NoContent if the application is successfully deleted.
        /// NotFound if the deletion fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
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

        /// <summary>
        /// Updates the approval status of an application in the database by ID.
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>NoContent if the request is successful.
        /// NotFound if the request fails.</returns>
        [HttpPut("{id}/approve")]
        [Authorize]
        public async Task<IActionResult> UpdateApplicationApprovalStatus(int id)
        {
            try
            {
                await _applicationService.UpdateApprovalStatus(id);
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
    }
}
