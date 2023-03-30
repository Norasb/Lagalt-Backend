using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Message;
using Lagalt_Backend.Services.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/messages")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all messages from the database.
        /// </summary>
        /// <returns>List of MessageDTOs</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetAllMessages()
        {
            return Ok(
                _mapper.Map<List<MessageDto>>(
                await _messageService.GetAllAsync()));
        }

        /// <summary>
        /// Get a specific message from the database.
        /// </summary>
        /// <param name="id">Message ID</param>
        /// <returns>MessageDTO</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MessageDto>> GetMessageById(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<MessageDto>(
                    await _messageService.GetByIdAsync(id)));

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

        /// <summary>
        /// Adds a message to the database.
        /// </summary>
        /// <param name="postMessage">MessagePostDTO</param>
        /// <returns>MessageDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddMessage(MessagePostDto postMessage)
        {
            Message message = new Message()
            {
                DOC = DateTime.Now,
                Text = postMessage.Text,
            };

            await _messageService.AddAsync(_mapper.Map<Message>(message));
            return CreatedAtAction("GetMessageById", new { id = message.Id }, message);
        }

        /// <summary>
        /// Update a message in the database by ID.
        /// </summary>
        /// <param name="id">Message ID</param>
        /// <param name="putMessage">MessagePutDTO</param>
        /// <returns>NoContent if the update is successful.
        /// Notfound if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateMessage(int id, MessagePutDto putMessage)
        {
            Message existingMessage = _messageService.GetByIdAsync(id).Result;

            existingMessage.Id = id;
            existingMessage.Text = putMessage.Text;
            existingMessage.DOC = DateTime.Now;

            try
            {
                await _messageService.UpdateAsync(_mapper.Map<Message>(existingMessage));
                return NoContent();
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

        /// <summary>
        /// Deletes a message from the database by ID.
        /// </summary>
        /// <param name="id">Message ID</param>
        /// <returns>NoContent if the request is successful.
        /// NotFound if the request fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messageService.DeleteByIdAsync(id);
                return NoContent();
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
    }
}
