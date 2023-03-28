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

namespace Lagalt_Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(_mapper.Map<List<UserDTO>>(await _userService.GetAllAsync()));
        }

        // GET: api/User/5
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

        [HttpGet("{id}/applications")]
        public async Task<ActionResult<IEnumerable<ApplicationsInUserDto>>> GetUsersApplications(string id)
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

        [HttpGet("{id}/projects")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectByUserId(string id)
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
                    Status = ((int)HttpStatusCode.NotFound)
                });
            }
        }

        [HttpGet("{id}/portfolio")]
        public async Task<ActionResult<PortfolioDTO>> GetPortfolioByUserName(string userName)
        {
            try
            {
                return Ok(_mapper.Map<PortfolioDTO>(await _userService.GetPortfolioInUser(userName)));
            } catch(Exception ex)
            {
                return NotFound(new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = ((int)HttpStatusCode.NotFound)
                });
            }
        }




        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserPostDTO>> PostUser(UserPostDTO userDto)
        {
            User user = _mapper.Map<User>(userDto);
            await _userService.AddAsync(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
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
