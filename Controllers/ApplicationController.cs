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

namespace Lagalt_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: api/Application
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return Ok(await _applicationService.GetAllAsync());
        }

        // GET: api/Application/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            try
            {
                return Ok(await _applicationService.GetByIdAsync(id));
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            try
            {
                await _applicationService.UpdateAsync(application);
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
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
