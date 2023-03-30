using Microsoft.AspNetCore.Mvc;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.UserServices;
using System.Net;
using AutoMapper;
using Lagalt_Backend.Models.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Lagalt_Backend.Models.Dto.Application;
using Lagalt_Backend.Models.Dto.Projects;
using Lagalt_Backend.Models.Dto.Portfolio;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users in the database.
        /// </summary>
        /// <returns>List of UserDTOs</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(_mapper.Map<List<UserDTO>>(await _userService.GetAllAsync()));
        }

        /// <summary>
        /// Get a specific user from the database by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>UserDTO or NotFound if the request fails.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            try
            {
                return Ok(_mapper.Map<UserDTO>(await _userService.GetByIdAsync(id)));
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

        /// <summary>
        /// Get a users applications by the User ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>List of ApplicationsInUserDTOs if the request is successful. 
        /// NotFound if the request fails.</returns>
        [HttpGet("{id}/applications")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ApplicationsInUserDto>>> GetUserApplications(string id)
        {
            try
            {
                return Ok(
                    _mapper.Map<List<ApplicationsInUserDto>>(
                        await _userService.GetApplicationsInUser(id)));
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
        /// Get a user's projects by the User ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>List of ProjectDTO or NotFound if the request fails.</returns>
        [HttpGet("{id}/projects")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsByUserId(string id)
        {
            try
            {
                return Ok(_mapper.Map<List<ProjectDto>>(await _userService.GetProjectsInUser(id)));
                
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }
        
        /// <summary>
        /// Get project owned by a specific user by the User ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>List of ProjectDTOs</returns>
        [HttpGet("{id}/OwnedProjects")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetOnlyOwnedProjectsInUser(string id)
        {
            try
            {
                return Ok(_mapper.Map<List<ProjectDto>>(await _userService.GetOnlyOwnedProjectsInUser(id)));

            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.NotFound
                });
            }
        }

        /// <summary>
        /// Get a user's portfolio by User ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>List of PortfolioDTO or NotFound if the request fails.</returns>
        [HttpGet("{id}/portfolio")]
        public async Task<ActionResult<PortfolioDTO>> GetPortfolioByUserId(string id)
        {
            try
            {
                return Ok(_mapper.Map<PortfolioDTO>(await _userService.GetPortfolioInUser(id)));
            } catch(Exception ex)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = ((int)HttpStatusCode.NotFound)
                });
            }
        }

        /// <summary>
        /// Update a user in the database by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="userDto">UserPutDTO</param>
        /// <returns>BadRequest if the ID in the DTO does not match the ID in the URL.
        /// NoContent if the update is successful.
        /// NotFound if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(string id, UserPutDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userService.UpdateAsync(_mapper.Map<User>(userDto));
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = ((int)HttpStatusCode.NotFound)
                });
                
            };
        }

        /// <summary>
        /// Add a user to the database.
        /// </summary>
        /// <param name="userDto">UserPostDTO</param>
        /// <returns>UserDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserPostDTO>> PostUser(UserPostDTO userDto)
        {
            User user = _mapper.Map<User>(userDto);
            await _userService.AddAsync(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        /// <summary>
        /// Delete a user from the database by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>NoContent if the request is successful.
        /// NotFound if the request fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    });
            }
        }
    }
}
